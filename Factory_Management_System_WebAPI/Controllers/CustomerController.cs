using Factory_Management_System_WebAPI.Data;
using Factory_Management_System_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Factory_Management_System_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        #region Get All Customer

        [HttpGet]
        public IActionResult GetAllCustomers(int AdminID)
        {
            ApiResponse response = null;
            var customers = _customerRepository.SelectAll(AdminID);
            if (customers == null)
            {
                response = new ApiResponse("Customers not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(customers, "Customer retrived successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get Customer By ID

        [HttpGet("{CustomerID}")]
        public IActionResult GetCustomerByID(int CustomerID)
        {
            ApiResponse response = null;
            var customer = _customerRepository.SelectByPK(CustomerID);
            if(customer == null)
            {
                response = new ApiResponse("Customer not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(customer, "Customer Retrived Successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Customer Detail

        [HttpPatch("personal-detail/{CustomerID}")]
        public IActionResult UpdateCustomerDetail([FromBody] CustomerModel customer ,int CustomerID)
        {
            ApiResponse response = null;
            if(customer == null)
            {
                response = new ApiResponse("Customer Details are required", 400);
                return BadRequest(response);
            }

            if(customer.CustomerID != CustomerID)
            {
                response = new ApiResponse("CustomerID missmatch", 400);
                return BadRequest(response);
            }

            var isUpdated = _customerRepository.UpdateDetail(customer);
            if(!isUpdated)
            {
                response = new ApiResponse("Error while updating customer detail", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Customer Detail Update Succesfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Bank Detail
        [HttpPatch("bank-detail/{CustomerID}")]
        public IActionResult UpdateBankDetail([FromBody] CustomerModel customer, int CustomerID)
        {
            ApiResponse response = null;
            if (customer == null)
            {
                response = new ApiResponse("Customer Details are required", 400);
                return BadRequest(response);
            }

            if (customer.CustomerID != CustomerID)
            {
                response = new ApiResponse("CustomerID missmatch", 400);
                return BadRequest(response);
            }

            var isUpdated = _customerRepository.UpdateBankDetail(customer);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating customer detail", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Customer Detail Update Succesfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Company Detail

        [HttpPatch("company-detail/{CustomerID}")]
        public IActionResult UpdateCompanyDetail([FromBody] CustomerModel customer, int CustomerID)
        {
            ApiResponse response = null;
            if (customer == null)
            {
                response = new ApiResponse("Customer Details are required", 400);
                return BadRequest(response);
            }

            if (customer.CustomerID != CustomerID)
            {
                response = new ApiResponse("CustomerID missmatch", 400);
                return BadRequest(response);
            }

            var isUpdated = _customerRepository.UpdateCompanyDetail(customer);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating customer detail", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Customer Detail Update Succesfully", 200);
            return Ok(response);
        }
        #endregion

        #region Change Current Password

        [HttpPatch("chnage-password/{CustomerID}")]

        public IActionResult ChangeCurrentPassword(int CustomerID, string Password, string ConfirmPassword)
        {
            ApiResponse response = null;
            if(CustomerID == null)
            {
                response = new ApiResponse("CustomerID is required", 400);
                return BadRequest(response);
            }

            if(Password == null)
            {
                response = new ApiResponse("Password feild is required", 400);
                return BadRequest(response);
            }

            if(ConfirmPassword == null)
            {
                response = new ApiResponse("Confirm Password is required", 400);
                return BadRequest(response);
            }

            if(ConfirmPassword != Password)
            {
                response = new ApiResponse("Confirm Password miss match", 400);
                return BadRequest(response);
            }

            bool isUpdated = _customerRepository.ChangePassword(CustomerID, Password);

            if(!isUpdated)
            {
                response = new ApiResponse("Error while updating password", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Password update successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Insert Customer

        [HttpPost]
        public IActionResult Insert(CustomerModel customer)
        {
            ApiResponse response = null;
            if(customer == null)
            {
                response = new ApiResponse("Customer Details are required", 400);
                return BadRequest(response);
            }

            bool isInserted = _customerRepository.Insert(customer);

            if (!isInserted)
            {
                response = new ApiResponse("Error while inserting customer detail", 500);
                return BadRequest();
            }

            response = new ApiResponse("Customer Detail Insert Succesfully", 200);
            return Ok(response);
        }
        #endregion

        #region Delete Customer
        [HttpPost("delete/{CustomerID}")]
        public IActionResult Delete(int CustomerID)
        {
            ApiResponse response = null;
            if (CustomerID <= 0 || CustomerID == null)
            {
                response = new ApiResponse("CustomerID is Required", 404);
                return BadRequest(response);
            }

            bool isDeleted = _customerRepository.Delete(CustomerID);

            if (!isDeleted)
            {
                response = new ApiResponse("Error while deleting customer", 404);
                return BadRequest(response);
            }

            response = new ApiResponse("Customer Delete Successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}
