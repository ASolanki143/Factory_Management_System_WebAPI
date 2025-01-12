using Factory_Management_System_WebAPI.Data;
using Factory_Management_System_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Factory_Management_System_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;
        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        #region Get All Orders
        [HttpGet]
        public IActionResult GetAllOrder(int AdminID)
        {
            ApiResponse response = null;
            var orders = _orderRepository.SelectAll(AdminID);
            if(orders == null)
            {
                response = new ApiResponse("Order Not Found", 400);
                return BadRequest(response);
            }
            response = new ApiResponse(orders, "Order Retrived Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get All Order By CustomerID
        [HttpGet("/Customer/{CustomerID}")]
        public IActionResult GetAllOrderByCustomerID(int CustomerID)
        {
            ApiResponse response = null;
            if(CustomerID == null)
            {
                response = new ApiResponse("CustomerID is required", 400);
                return BadRequest(response);
            }
            var orders = _orderRepository.SelectByCustomerID(CustomerID);
            if (orders == null)
            {
                response = new ApiResponse("Order Not Found", 400);
                return BadRequest(response);
            }
            response = new ApiResponse(orders, "Order Retrived Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get All Order By PK
        [HttpGet("{OrderID}")]
        public IActionResult GetAllOrderByID(int OrderID)
        {
            ApiResponse response = null;
            if (OrderID == null)
            {
                response = new ApiResponse("OrderID is required", 400);
                return BadRequest(response);
            }
            var orders = _orderRepository.SelectByPK(OrderID);
            if (orders == null)
            {
                response = new ApiResponse("Order Not Found", 400);
                return BadRequest(response);
            }
            response = new ApiResponse(orders, "Order Retrived Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Order Status
        [HttpPatch("OrderStatus/{OrderID}")]
        public IActionResult UpdateOrderStatus(int OrderID)
        {
            ApiResponse response = null;
            if (OrderID == null)
            {
                response = new ApiResponse("OrderID is required", 400);
                return BadRequest(response);
            }
            bool isUpdated = _orderRepository.UpdateOrderStatus(OrderID);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating order status", 500);
                return BadRequest(response);
            }
            response = new ApiResponse("Order Updated Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Order Amount
        [HttpPatch("OrderAmount/{OrderID}")]
        public IActionResult UpdateOrderAmount(int OrderID)
        {
            ApiResponse response = null;
            if (OrderID == null)
            {
                response = new ApiResponse("OrderID is required", 400);
                return BadRequest(response);
            }
            bool isUpdated = _orderRepository.UpdateAmounts(OrderID);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating order amount", 500);
                return BadRequest(response);
            }
            response = new ApiResponse("Order Updated Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Insert Order
        [HttpPost]
        public IActionResult InsertOrder(int CustomerID, int AdminID)
        {
            ApiResponse response = null;
            if(CustomerID == null)
            {
                response = new ApiResponse("CustomerID feild is required", 400);
                return BadRequest(response);
            }

            int OrderID = _orderRepository.Insert(CustomerID,AdminID);
            if (OrderID <= 0 || OrderID == null)
            {
                response = new ApiResponse("Error while inserting order", 500);
                return BadRequest(response);
            }
            response = new ApiResponse("Order Inserted Successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}
