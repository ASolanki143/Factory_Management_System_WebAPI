using Factory_Management_System_WebAPI.Data;
using Factory_Management_System_WebAPI.Middlewares;
using Factory_Management_System_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Factory_Management_System_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtGenerator;
        private readonly AuthRepository _authRepository;
        public AuthController(AuthRepository authRepository, JwtTokenGenerator jwtGenerator)
        {
            _authRepository = authRepository;
            _jwtGenerator = jwtGenerator;
        }

        #region Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthModel login)
        {
            ApiResponse response = null;
            if (login == null)
            {
                response = new ApiResponse("Login details not found", 400);
                return BadRequest(response);
            }
            LoginResponse loginResponse = _authRepository.Login(login);
            if(loginResponse.Status == 0)
            {
                response = new ApiResponse(loginResponse.Message, 400);
                return BadRequest(response);
            }
            int UserID = loginResponse.UserID;
            String role = login.Role;

            var user = _authRepository.GetUser(role, UserID);
            var token = _jwtGenerator.GenerateToken(user);
            bool isSet = _authRepository.SetAccessToken(role,token,UserID);
            if (!isSet)
            {
                response = new ApiResponse("Token not set", 400);
                return BadRequest(response);
            }
            response = new ApiResponse(token, "Login Successfully", 200);
            return Ok(response);

        }
        #endregion

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                var user = HttpContext.Items["User"] as ClaimsPrincipal;
                Console.WriteLine($"user : {user}");
                if (user == null)
                {
                    return Unauthorized(new ApiResponse("User not found", 401));
                }

                // Extract UserID and Role from the claims
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = user.FindFirst(ClaimTypes.Role)?.Value;

                Console.WriteLine($"userId : {userId}");
                Console.WriteLine($"role : {role}");
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
                {
                    return Unauthorized(new ApiResponse("Invalid user claims", 401));
                }

                // Call stored procedure to remove the token from the database
                bool isTokenRemoved = _authRepository.RemoveAccessToken(role, Convert.ToInt32(userId));

                if (isTokenRemoved)
                {
                    // Remove the token from the cookies (client side)
                    Response.Cookies.Delete("accessToken");

                    return Ok(new ApiResponse("Logout successful", 200));
                }

                return BadRequest(new ApiResponse("Failed to log out", 400));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(ex.Message, 500));
            }
        }
    }
}
