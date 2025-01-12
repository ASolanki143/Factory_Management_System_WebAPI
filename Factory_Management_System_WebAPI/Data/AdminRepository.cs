using Factory_Management_System_WebAPI.Models;
using System.Data.SqlClient;

namespace Factory_Management_System_WebAPI.Data
{
    public class AdminRepository
    {
        private readonly string _connectionString;

        public AdminRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Select All
        public IEnumerable<AdminModel> SelectAll()
        {
            var admins = new List<AdminModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Admin_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    admins.Add(new AdminModel()
                    {
                        AdminID = Convert.ToInt32(reader["AdminID"]),
                        AdminName = reader["AdminName"].ToString(),
                        Email = reader["Email"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Address = reader["Address"].ToString(),
                        //Password = reader["Password"].ToString(),
                        CompanyName = reader["CompanyName"].ToString(),
                        GSTNo = reader["GSTNo"].ToString(),
                        CompanyNo = reader["CompanyNo"].ToString(),
                        CompanyEmail = reader["CompanyEmail"].ToString(),
                        BankName = reader["BankName"].ToString(),
                        AccountNo = reader["AccountNo"].ToString(),
                        IFSCCode = reader["IFSCCode"].ToString()
                    });
                }
            }
            return admins;
        }
        #endregion

        #region Select By PK
        public AdminModel SelectByPK(int AdminID)
        {
            AdminModel admin = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Admin_SelectByPK", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@AdminID", AdminID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    admin = new AdminModel()
                    {
                        AdminID = Convert.ToInt32(reader["AdminID"]),
                        AdminName = reader["AdminName"].ToString(),
                        Email = reader["Email"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Address = reader["Address"].ToString(),
                        //Password = reader["Password"].ToString(),
                        CompanyName = reader["CompanyName"].ToString(),
                        GSTNo = reader["GSTNo"].ToString(),
                        CompanyNo = reader["CompanyNo"].ToString(),
                        CompanyEmail = reader["CompanyEmail"].ToString(),
                        BankName = reader["BankName"].ToString(),
                        AccountNo = reader["AccountNo"].ToString(),
                        IFSCCode = reader["IFSCCode"].ToString(),
                    };
                }
            }
            return admin;
        }
        #endregion

        #region Update Admin Detail
        public bool UpdateDetail(AdminModel admin)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Admin_UpdateDetail", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AdminID", admin.AdminID);
                cmd.Parameters.AddWithValue("@AdminName", admin.AdminName);
                cmd.Parameters.AddWithValue("@Address", admin.Address);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@MobileNo", admin.MobileNo);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update Bank Detail
        public bool UpdateBankDetail(AdminModel admin)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Admin_UpdateBankDetail", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AdminID", admin.AdminID);
                cmd.Parameters.AddWithValue("@AccountNo", admin.AccountNo);
                cmd.Parameters.AddWithValue("@BankName", admin.BankName);
                cmd.Parameters.AddWithValue("@IFSCCode", admin.IFSCCode);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update Company Detail
        public bool UpdateCompanyDetail(AdminModel admin)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Admin_UpdateCompanyDetail", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AdminID", admin.AdminID);
                cmd.Parameters.AddWithValue("@CompanyName", admin.CompanyName);
                cmd.Parameters.AddWithValue("@GSTNo", admin.GSTNo);
                cmd.Parameters.AddWithValue("@CompanyNo", admin.CompanyNo);
                cmd.Parameters.AddWithValue("@CompanyEmail", admin.CompanyEmail);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Change Password
        public bool ChangePassword(int AdminID, string Password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Admin_ChangePassword", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AdminID", AdminID);
                cmd.Parameters.AddWithValue("@Password", Password);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Insert
        public bool Insert(AdminModel admin)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Admin_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.Parameters.AddWithValue("@AdminName", admin.AdminName);
                cmd.Parameters.AddWithValue("@Address", admin.Address);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@MobileNo", admin.MobileNo);
                cmd.Parameters.AddWithValue("@AccountNo", admin.AccountNo);
                cmd.Parameters.AddWithValue("@BankName", admin.BankName);
                cmd.Parameters.AddWithValue("@IFSCCode", admin.IFSCCode);
                cmd.Parameters.AddWithValue("@CompanyName", admin.CompanyName);
                cmd.Parameters.AddWithValue("@GSTNo", admin.GSTNo);
                cmd.Parameters.AddWithValue("@CompanyNo", admin.CompanyNo);
                cmd.Parameters.AddWithValue("@CompanyEmail", admin.CompanyEmail);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Delete
        public bool Delete(int AdminID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Admin_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AdminID", AdminID);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion
    }
}
