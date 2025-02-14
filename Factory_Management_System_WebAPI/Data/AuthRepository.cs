using Factory_Management_System_WebAPI.Models;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;

namespace Factory_Management_System_WebAPI.Data
{
    public class AuthRepository
    {
        private readonly string _connectionString;
        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region login
        public LoginResponse Login(AuthModel auth)
        {
            LoginResponse response = new LoginResponse();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_Login", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", auth.Email);
                cmd.Parameters.AddWithValue("@Password", auth.Password);
                cmd.Parameters.AddWithValue("@Role", auth.Role);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    response.Status = Convert.ToInt32(reader["Status"]);
                    response.Message = reader["Message"].ToString();
                    response.UserID = Convert.ToInt32(reader["UserID"]);
                }
            }
            return response;
        }
        #endregion

        #region Get Admin
        public UserModel GetUser(string Role, int UserID)
        {
            UserModel user = new UserModel();
            user.UserID = UserID;
            user.Role = Role;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Get_User", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Role", Role);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if(Role == "Customer")
                    {
                        user.AdminID = Convert.ToInt32(reader["AdminID"]);
                        user.Name = reader["CustomerName"].ToString();
                        user.Email = reader["Email"].ToString();
                    }
                    else
                    {
                        user.Name = reader["AdminName"].ToString();
                        user.Email = reader["Email"].ToString();
                    }
                }
            }
            return user;
        }
        #endregion

        #region Logout
        public bool Logout(string Role, int UserID)
        {
            return RemoveAccessToken(Role, UserID);
        }
        #endregion

        #region Set Access Token
        public bool SetAccessToken(string Role, string Token, int UserID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Set_AccessToken", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@Role", Role);
                    cmd.Parameters.AddWithValue("@Token", Token);
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    conn.Open();

                    // ExecuteScalar to get affected rows
                    int rowAffected = Convert.ToInt32(cmd.ExecuteScalar());

                    return rowAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Log error for debugging
                Console.WriteLine($"Error in SetAccessToken: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Get Access Token
        public string GetAccessToken(string Role, int UserID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Get_AccessToken", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Role", Role);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                conn.Open();

                string token = cmd.ExecuteScalar().ToString();
                return token;
            }
        }
        #endregion

        #region Remove Access Token
        public bool RemoveAccessToken(string Role, int UserID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Remove_AccessToken", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@Role", Role);
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    conn.Open();

                    // Execute Scalar to get the affected rows count
                    int rowAffected = Convert.ToInt32(cmd.ExecuteScalar());

                    return rowAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Log error for debugging
                Console.WriteLine($"Error in RemoveAccessToken: {ex.Message}");
                return false;
            }
        }

        #endregion
    }
}
