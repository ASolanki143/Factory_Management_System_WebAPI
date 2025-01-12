using Factory_Management_System_WebAPI.Models;
using System.Data.SqlClient;

namespace Factory_Management_System_WebAPI.Data
{
    public class CustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Select All
        public IEnumerable<CustomerModel> SelectAll(int AdminID)
        {
            var customers = new List<CustomerModel>();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AdminID", AdminID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new CustomerModel()
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),
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
                        AdminID = Convert.ToInt32(reader["AdminID"]),
                        AdminName = reader["AdminName"].ToString()
                    });

                }
            }
            return customers;
        }
        #endregion

        #region Select By PK
        public CustomerModel SelectByPK(int CustomerID)
        {
            CustomerModel customer = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_SelectByPK", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new CustomerModel()
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString(),
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
                        AdminID = Convert.ToInt32(reader["AdminID"]),
                        AdminName = reader["AdminName"].ToString()
                    };
                }
            }
            return customer;
        }
        #endregion

        #region Update Customer Detail
        public bool UpdateDetail(CustomerModel customer)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_UpdateDetail", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID",customer.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@MobileNo", customer.MobileNo);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update Bank Detail
        public bool UpdateBankDetail(CustomerModel customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_UpdateBankDetail", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("@AccountNo", customer.AccountNo);
                cmd.Parameters.AddWithValue("@BankName", customer.BankName);
                cmd.Parameters.AddWithValue("@IFSCCode", customer.IFSCCode);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update Company Detail
        public bool UpdateCompanyDetail(CustomerModel customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_UpdateCompanyDetail", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                cmd.Parameters.AddWithValue("@GSTNo", customer.GSTNo);
                cmd.Parameters.AddWithValue("@CompanyNo", customer.CompanyNo);
                cmd.Parameters.AddWithValue("@CompanyEmail", customer.CompanyEmail);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Change Password
        public bool ChangePassword(int CustomerID, string Password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_ChangePassword", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@Password", Password);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Insert
        public bool Insert(CustomerModel customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Password", customer.Password);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                cmd.Parameters.AddWithValue("@AccountNo", customer.AccountNo);
                cmd.Parameters.AddWithValue("@BankName", customer.BankName);
                cmd.Parameters.AddWithValue("@IFSCCode", customer.IFSCCode);
                cmd.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                cmd.Parameters.AddWithValue("@GSTNo", customer.GSTNo);
                cmd.Parameters.AddWithValue("@CompanyNo", customer.CompanyNo);
                cmd.Parameters.AddWithValue("@CompanyEmail", customer.CompanyEmail);
                cmd.Parameters.AddWithValue("@AdminID",customer.AdminID);
                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Delete
        public bool Delete(int CustomerID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion
    }
}
