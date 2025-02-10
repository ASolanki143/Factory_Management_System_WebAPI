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
        public IActionResult GetAllOrder(int AdminID, string? CustomerName , string? status , double? MinValue, double? MaxValue, DateTime? StartDate, DateTime? EndDate )
        {
            ApiResponse response = null;
            var orders = _orderRepository.SelectAll(AdminID,CustomerName,status, MinValue,MaxValue,StartDate,EndDate);
            if(orders == null)
            {
                response = new ApiResponse("Order Not Found", 400);
                return BadRequest(response);
            }
            response = new ApiResponse(orders, "Order Retrived Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get All Orders
        [HttpGet("OrderRequest")]
        public IActionResult GetAllOrderRequest(int AdminID, string? CustomerName, string? status, double? MinValue, double? MaxValue, DateTime? StartDate, DateTime? EndDate)
        {
            ApiResponse response = null;
            var orders = _orderRepository.SelectAllOrderRequest(AdminID, CustomerName, status, MinValue, MaxValue, StartDate, EndDate);
            if (orders == null)
            {
                response = new ApiResponse("Order Not Found", 400);
                return BadRequest(response);
            }
            response = new ApiResponse(orders, "Order Retrived Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get All Order By CustomerID
        [HttpGet("Customer/{CustomerID}")]
        public IActionResult GetAllOrderByCustomerID(int CustomerID,int AdminID)
        {
            ApiResponse response = null;
            if(CustomerID == null)
            {
                response = new ApiResponse("CustomerID is required", 400);
                return BadRequest(response);
            }
            var orders = _orderRepository.SelectByCustomerID(CustomerID,AdminID);
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
        [HttpPatch("{OrderID}/status/{NewStatus}")]
        public IActionResult UpdateOrderStatus(int OrderID, string NewStatus, int AdminID)
        {
            ApiResponse response = null;

            // 🔹 Fix: OrderID can't be null, so check for invalid values
            if (OrderID <= 0)
            {
                response = new ApiResponse("Invalid OrderID", 400);
                return BadRequest(response);
            }

            // 🔹 Call repository method that returns an int status code
            int updateResult = _orderRepository.UpdateOrderStatus(OrderID, NewStatus, AdminID);

            // 🔹 Handle SQL return values properly
            if (updateResult == 0)
            {
                response = new ApiResponse("Order not found or status unchanged", 404);
                return NotFound(response);
            }
            else if (updateResult == -1)
            {
                response = new ApiResponse("Database error occurred", 500);
                return StatusCode(500, response);
            }
            else if (updateResult == -2)
            {
                response = new ApiResponse("Bill insertion failed", 500);
                return StatusCode(500, response);
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
            Console.WriteLine($"{CustomerID}, {AdminID}");
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
            response = new ApiResponse(OrderID.ToString(),"Order Inserted Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Order Request
        [HttpPatch("OrderRequest/{OrderID}")]
        public IActionResult UpdateOrderRequest(int OrderID, string RequestStatus)
        {
            ApiResponse response = null;
            if (OrderID == null)
            {
                response = new ApiResponse("OrderID is required", 400);
                return BadRequest(response);
            }
            bool isUpdated = _orderRepository.UpdateRequestStatus(OrderID, RequestStatus);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating order request", 500);
                return BadRequest(response);
            }
            response = new ApiResponse("Order Updated Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region delete Order
        [HttpDelete("{OrderID}")]
        public IActionResult DeleteOrder(int OrderID)
        {
            ApiResponse response = null;
            if (OrderID == null || OrderID < 1)
            {
                response = new ApiResponse("OrderID is required", 400);
                return BadRequest(response);
            }
            bool isDeleted = _orderRepository.Delete(OrderID);
            if (!isDeleted)
            {
                response = new ApiResponse("Error while deleting order", 500);
                return BadRequest(response);
            }
            response = new ApiResponse("Order Deleted Successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}
