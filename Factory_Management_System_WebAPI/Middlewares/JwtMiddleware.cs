using Factory_Management_System_WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Data;

namespace Factory_Management_System_WebAPI.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var path = context.Request.Path.Value;
                Console.WriteLine($"Path : {path}");
                if (path.StartsWith("/api/Auth/login", System.StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Login-----");
                    await _next(context);
                    return;
                }

                var token = context.Request.Cookies["accessToken"] ??
                            context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();

                Console.WriteLine($"Token : {token}");
                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new ApiResponse("Unauthorized", 401));
                    return;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var rawKey = _configuration["Jwt:Key"];
                Console.WriteLine($"Raw Key: '{rawKey}'");

                if (string.IsNullOrWhiteSpace(rawKey))
                {
                    throw new Exception("JWT Key is missing or empty!");
                }

                var trimmedKey = rawKey.Trim();
                var keyBytes = Encoding.UTF8.GetBytes(trimmedKey);

                Console.WriteLine($"Key Bytes: {string.Join(", ", keyBytes)}");

                
                    var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidAudience = _configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    }, out _);

                    Console.WriteLine("Token is valid.");

                foreach (var claim in claimsPrincipal.Claims)
                {
                    Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                }

                var nameId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Console.WriteLine($"NameId: {nameId}");

                var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;

                Console.WriteLine($"NameId : {Convert.ToInt32(nameId)}");
                Console.WriteLine($"Role : {role}");

                if (string.IsNullOrEmpty(nameId) || string.IsNullOrEmpty(role))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new ApiResponse("Unauthorized", 401));
                    return;
                }
                string verifyToken = GetAccessToken(role.ToString(), Convert.ToInt32(nameId));

                Console.WriteLine($"Verify Token : {verifyToken}");
                if (verifyToken.Length != token.Length)
                {
                    Console.WriteLine($"Length Mismatch: VerifyToken={verifyToken.Length}, Token={token.Length}");
                }

                if (verifyToken != token)
                {
                    Console.WriteLine($"Verify Token : {verifyToken}");

                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new ApiResponse("Unauthorized", 401));
                    return;
                }

                context.Items["User"] = claimsPrincipal;
            }
            catch (SecurityTokenException ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal Server Error");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new ApiResponse(ex.Message, 500));
                return;
            }

            await _next(context);
        }

        #region Get Access Token
        public string GetAccessToken(string Role, int UserID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Get_AccessToken", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@Role", Role);
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    conn.Open();

                    // Handle NULL values safely
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToString(result) : null;
                }
            }
            catch (Exception ex)
            {
                // Log error for debugging
                Console.WriteLine($"Error in GetAccessToken: {ex.Message}");
                return null;
            }
        }

        #endregion
    }
}
