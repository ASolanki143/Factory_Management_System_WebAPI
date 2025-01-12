using Factory_Management_System_WebAPI.Data;
using Factory_Management_System_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Factory_Management_System_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemRepository _orderItemRepository;
        public OrderItemController(OrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        #region Get All Order Item
        [HttpGet]
        public IActionResult GetAllOrderItems(int AdminID)
        {
            ApiResponse response = null;
            var orderItems = _orderItemRepository.SelectAll(AdminID);
            if(orderItems == null)
            {
                response = new ApiResponse("Order Item Not Found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(orderItems, "Order Items Retrived Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get Order Item By ID
        [HttpGet("{OrderItemID}")]
        public IActionResult GetOrderItemByID(int OrderItemID)
        {
            ApiResponse response = null;
            if(OrderItemID <= 0 || OrderItemID == null)
            {
                response = new ApiResponse("OrderItemID is required", 400);
                return BadRequest(response);
            }
            var orderItem = _orderItemRepository.SelectByPK(OrderItemID);
            if (orderItem == null)
            {
                response = new ApiResponse("Order Item Not Found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(orderItem, "Order Item Retrived Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get Order Item By Order ID
        [HttpGet("order/{OrderID}")]
        public IActionResult GetOrderItemByOrderID(int OrderID)
        {
            ApiResponse response = null;
            if (OrderID <= 0 || OrderID == null)
            {
                response = new ApiResponse("OrderItemID is required", 400);
                return BadRequest(response);
            }
            var orderItem = _orderItemRepository.SelectByOrderID(OrderID);
            if (orderItem == null)
            {
                response = new ApiResponse("Order Item Not Found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(orderItem, "Order Item Retrived Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Insert
        [HttpPost]
        public IActionResult InsertOrderItem(int ProductID,int Quantity,int AdminID)
        {
            ApiResponse response = null;
            if(ProductID == null || Quantity == 0 || Quantity == null)
            {
                response = new ApiResponse("All Feild is required", 400);
                return BadRequest(response);
            }

            int OrderItemID = _orderItemRepository.Insert(ProductID, Quantity, AdminID);
            if (OrderItemID <= 0)
            {
                response = new ApiResponse("Error occur while inserting order item", 500);
                return BadRequest(response);
            }

            response = new ApiResponse(OrderItemID,"Order Item Insert Successfullu", 200);
            return Ok(response);
        }
        #endregion

        #region Update OrderID
        [HttpPatch("UpdateOrderID/{OrderItemID}")]
        public IActionResult UpdateOrderID(int OrderItemID,int OrderID)
        {
            ApiResponse response = null;
            if(OrderID == null || OrderItemID == null || OrderItemID <= 0)
            {
                response = new ApiResponse("All feild is required", 400);
                return BadRequest(response);
            }

            bool isUpdated = _orderItemRepository.UpdateOrderID(OrderID, OrderItemID);
            if(!isUpdated)
            {
                response = new ApiResponse("Error occus while updating OrderID", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("OrderID updating Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Order Item
        [HttpPatch("Update/{OrderItemID}")]
        public IActionResult UpdateOrderItem(int OrderItemID, [FromBody] OrderItemModel orderItem)
        {
            ApiResponse response = null;
            if(OrderItemID <= 0 || OrderItemID == null)
            {
                response = new ApiResponse("OrderItemID is required", 400);
                return BadRequest(response);
            }

            if(orderItem.OrderItemID != OrderItemID)
            {
                response = new ApiResponse("OrderItemID missmatch", 400);
                return BadRequest(response);
            }
            if(orderItem == null)
            {
                response = new ApiResponse("All feild is required", 400);
                return BadRequest(response);
            }

            bool isUpdated = _orderItemRepository.Update(orderItem);
            if(!isUpdated)
            {
                response = new ApiResponse("Error occur while updating Order Item", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Order Item update Successfully", 200);
            return Ok(response);
        }
        #endregion
        #region DeleteOrderItem
        [HttpDelete("{OrderItemID}")]
        public IActionResult DeleteOrderItem(int OrderItemID)
        {
            ApiResponse response = null;
            if(OrderItemID == null || OrderItemID <= 0)
            {
                response = new ApiResponse("OrderItemID is required", 400);
                return BadRequest(response);
            }

            bool isDeleted = _orderItemRepository.Delete(OrderItemID);
            if(!isDeleted)
            {
                response = new ApiResponse("Error occur while deleting OrderItem", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Order Item Deleted Successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}
