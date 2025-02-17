﻿using Factory_Management_System_WebAPI.Data;
using Factory_Management_System_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Factory_Management_System_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly BillRepository _billRepository;

        public BillController(BillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        #region Get All Bill 
        [HttpGet]
        public IActionResult GetAllBill(int AdminID, string Status = null, DateTime? StartDate = null, DateTime? EndDate = null,
            int? month = null, int? year = null,string? CustomerName = null, decimal? MinAmount = null, decimal? MaxAmount = null)
        {
            ApiResponse response = null;

            // Pass the filter parameters to the repository
            var bills = _billRepository.SelectAll(AdminID, Status, StartDate, EndDate, month, year, CustomerName, MinAmount, MaxAmount);

            if (bills == null || !bills.Any())  // Check if no bills were found
            {
                response = new ApiResponse("Bill not found", 404);
                return NotFound(response);
            }

            response = new ApiResponse(bills, "Bills retrieved successfully", 200);
            return Ok(response);
        }

        #endregion

        #region Get Bill By ID
        [HttpGet("{BillID}")]
        public IActionResult GetBillByID(int BillID)
        {
            ApiResponse response = null;
            if (BillID < 0 || BillID == null)
            {
                response = new ApiResponse("BillID not found", 400);
                return BadRequest(response);
            }
            var bill = _billRepository.SelectByPK(BillID);
            if (bill == null)
            {
                response = new ApiResponse("Bill not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(bill, "Bill retrived successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get Bill By CustomerID
        [HttpGet("Customer/{CustomerID}")]
        public IActionResult GetBillByCustomerID(int CustomerID, int AdminID)
        {
            ApiResponse response = null;
            if (CustomerID < 0 || CustomerID == null)
            {
                response = new ApiResponse("CustomerID not found", 400);
                return BadRequest(response);
            }
            var bill = _billRepository.SelectByCustomer(CustomerID, AdminID);
            if (bill == null)
            {
                response = new ApiResponse("Bill not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(bill, "Bill retrived successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get Bill By OrderIDIA
        [HttpGet("/Order/{OrderID}")]
        public IActionResult GetBillByOrderID(int OrderID)
        {
            ApiResponse response = null;
            if (OrderID < 0 || OrderID == null)
            {
                response = new ApiResponse("OrderID not found", 400);
                return BadRequest(response);
            }
            var bill = _billRepository.SelectByOrderID(OrderID);
            if (bill == null)
            {
                response = new ApiResponse("Bill not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(bill, "Bill retrived successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Insert Order
        [HttpPost("{OrderID}")]
        public IActionResult InsertOrder(int OrderID, int AdminID)
        {
            ApiResponse response = null;
            if (OrderID < 0 || OrderID == null)
            {
                response = new ApiResponse("OrderID is required", 404);
                return BadRequest(response);
            }
            int data = _billRepository.Insert(OrderID, AdminID);
            if (data == null)
            {
                response = new ApiResponse("Error while inserting bill", 500);
                return BadRequest(response);
            }
            response = new ApiResponse(data, "Bill Insert Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Order Status
        [HttpPatch("UpdateStatus/{BillID}")]
        public IActionResult UodateBillStatus(int BillID)
        {
            ApiResponse response = null;
            if (BillID < 0 || BillID == null)
            {
                response = new ApiResponse("BillID is required", 404);
                return BadRequest(response);
            }
            bool isUpdated = _billRepository.UpdateStatus(BillID);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating bill status", 500);
                return BadRequest(response);
            }
            response = new ApiResponse("Bill Updated Successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}
