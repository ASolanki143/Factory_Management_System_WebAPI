using Azure;
using Factory_Management_System_WebAPI.Data;
using Factory_Management_System_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Factory_Management_System_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminRepository _adminRepository;

        public AdminController(AdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        #region Get All Admin

        [HttpGet]
        public IActionResult GetAllAdmins()
        {
            ApiResponse response = null;
            var admins = _adminRepository.SelectAll();
            if (admins == null)
            {
                response = new ApiResponse("Admins not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(admins, "Admin retrived successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Get Admin By ID

        [HttpGet("{AdminID}")]
        public IActionResult GetAdminByID(int AdminID)
        {
            ApiResponse response = null;
            var admin = _adminRepository.SelectByPK(AdminID);
            if (admin == null)
            {
                response = new ApiResponse("Admin not found", 404);
                return NotFound(response);
            }
            response = new ApiResponse(admin, "Admin Retrived Successfully",200);
            return Ok(response);
        }
        #endregion

        #region Update Admin Detail

        [HttpPatch("personal-detail/{AdminID}")]
        public IActionResult UpdateAdminDetail([FromBody] AdminModel admin, int AdminID)
        {
            ApiResponse response = null;
            if (admin == null)
            {
                response = new ApiResponse("Admin Details are required", 400);
                return BadRequest(response);
            }

            if (admin.AdminID != AdminID)
            {
                response = new ApiResponse("AdminID missmatch", 400);
                return BadRequest(response);
            }

            var isUpdated = _adminRepository.UpdateDetail(admin);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating admin detail", 500);

                return BadRequest(response);
            }

            response = new ApiResponse("Admin Detail Update Succesfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Bank Detail
        [HttpPatch("bank-detail/{AdminID}")]
        public IActionResult UpdateBankDetail([FromBody] AdminModel admin, int AdminID)
        {
            ApiResponse response = null;
            if (admin == null)
            {
                response = new ApiResponse("Admin Details are required", 400);
                return BadRequest(response);
            }

            if (admin.AdminID != AdminID)
            {
                response = new ApiResponse("AdminID missmatch", 400);
                return BadRequest(response);
            }

            var isUpdated = _adminRepository.UpdateBankDetail(admin);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating admin detail", 500);
                return BadRequest(response);
            }
            response = new ApiResponse("Admin Detail Update Succesfully", 200);
            return Ok(response);
        }
        #endregion

        #region Update Company Detail

        [HttpPatch("company-detail/{AdminID}")]
        public IActionResult UpdateCompanyDetail([FromBody] AdminModel admin, int AdminID)
        {
            ApiResponse response = null;
            if (admin == null)
            {
                response = new ApiResponse("Admin Details are required", 400);
                return BadRequest(response);
            }

            if (admin.AdminID != AdminID)
            {
                response = new ApiResponse("AdminID missmatch", 400);
                return BadRequest("AdminID missmatch");
            }

            var isUpdated = _adminRepository.UpdateCompanyDetail(admin);
            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating admin detail", 400);
                return BadRequest(response);
            }
            response = new ApiResponse("Admin Detail Update Succesfully", 200);
            return Ok(response);
        }
        #endregion

        #region Change Current Password

        [HttpPatch("chnage-password/{AdminID}")]

        public IActionResult ChangeCurrentPassword(int AdminID, string Password, string ConfirmPassword)
        {
            ApiResponse response = null;
            if (AdminID == null)
            {
                response = new ApiResponse("AdminID is required", 400);
                return BadRequest(response);
            }

            if (Password == null)
            {
                response = new ApiResponse("Password feild is required", 400);
                return BadRequest(response);
            }

            if (ConfirmPassword == null)
            {
                response = new ApiResponse("Confirm Password is required", 400);
                return BadRequest(response);
            }

            if (ConfirmPassword != Password)
            {
                response = new ApiResponse("Confirm Password miss match", 400);
                return BadRequest(response);
            }

            bool isUpdated = _adminRepository.ChangePassword(AdminID, Password);

            if (!isUpdated)
            {
                response = new ApiResponse("Error while updating password", 500);
                return BadRequest(response);
            }

            response = new ApiResponse("Password update successfully", 200);
            return Ok(response);
        }
        #endregion

        #region Insert Admin

        [HttpPost]
        public IActionResult Insert(AdminModel admin)
        {
            ApiResponse response = null;
            if (admin == null)
            {
                response = new ApiResponse("Admin Details are required", 400);
                return BadRequest(response);
            }

            bool isInserted = _adminRepository.Insert(admin);

            if (!isInserted)
            {
                response = new ApiResponse("Error while inserting admin detail", 500);
                return BadRequest();
            }

            response = new ApiResponse("Admin Detail Insert Succesfully", 200);
            return Ok(response);
        }
        #endregion

        #region Delete Customer
        [HttpPost("delete/{AdminID}")]
        public IActionResult Delete(int AdminID)
        {
            ApiResponse response = null;
            if (AdminID <= 0 || AdminID == null)
            {
                response = new ApiResponse("AdminID is Required", 404);
                return BadRequest(response);
            }

            bool isDeleted = _adminRepository.Delete(AdminID);

            if (!isDeleted)
            {
                response = new ApiResponse("Error while deleting admin", 404);
                return BadRequest(response);
            }

            response = new ApiResponse("Admin Delete Successfully", 200);
            return Ok(response);
        }
        #endregion
    }
}
