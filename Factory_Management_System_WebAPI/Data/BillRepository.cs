using Factory_Management_System_WebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace Factory_Management_System_WebAPI.Data
{
    public class BillRepository
    {
        private readonly string _connectionString;
        public BillRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Select All
        public IEnumerable<BillModel> SelectAll(int AdminID, string Status = "All", DateTime? StartDate = null, DateTime? EndDate = null, int? Month = null, int? Year = null, string? CustomerName = null, decimal? MinAmount = null, decimal? MaxAmount = null)
        {
            var bills = new List<BillModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bill_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                // Adding the parameters to the stored procedure
                cmd.Parameters.AddWithValue("@AdminID", AdminID);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@StartDate", (object)StartDate ?? DBNull.Value);  // Use DBNull.Value if null
                cmd.Parameters.AddWithValue("@EndDate", (object)EndDate ?? DBNull.Value);      // Use DBNull.Value if null
                cmd.Parameters.AddWithValue("@Month", (object)Month ?? DBNull.Value);          // Use DBNull.Value if null
                cmd.Parameters.AddWithValue("@Year", (object)Year ?? DBNull.Value);            // Use DBNull.Value if null
                cmd.Parameters.AddWithValue("@CustomerName", (object)CustomerName ?? DBNull.Value); // Use DBNull.Value if null
                cmd.Parameters.AddWithValue("@MinAmount", (object)MinAmount ?? DBNull.Value);   // Use DBNull.Value if null
                cmd.Parameters.AddWithValue("@MaxAmount", (object)MaxAmount ?? DBNull.Value);   // Use DBNull.Value if null

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    bills.Add(new BillModel()
                    {
                        BillID = reader["BillID"] != DBNull.Value ? Convert.ToInt32(reader["BillID"]) : (int?)null,
                        InvoiceNo = reader["InvoiceNo"] != DBNull.Value ? reader["InvoiceNo"].ToString() : null,
                        BillDate = reader["BillDate"] != DBNull.Value ? Convert.ToDateTime(reader["BillDate"]) : (DateTime?)null,
                        Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : null,
                        OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : (int?)null,
                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"]) : (double?)null,
                        CGST = reader["CGST"] != DBNull.Value ? Convert.ToDouble(reader["CGST"]) : (double?)null,
                        SGST = reader["SGST"] != DBNull.Value ? Convert.ToDouble(reader["SGST"]) : (double?)null,
                        TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDouble(reader["TotalAmount"]) : (double?)null,
                        CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : null,
                        CompanyNo = reader["CompanyNo"] != DBNull.Value ? reader["CompanyNo"].ToString() : null,
                        AdminName = reader["AdminName"] != DBNull.Value ? reader["AdminName"].ToString() : null
                    });
                }
            }

            return bills;
        }

        #endregion

        #region Select By PK
        public BillModel SelectByPK(int BillID)
        {
            BillModel bill= new BillModel();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bill_SelectByPk", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BillID",BillID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    bill = new BillModel()
                    {
                        BillID = reader["BillID"] != DBNull.Value ? Convert.ToInt32(reader["BillID"]) : (int?)null,
                        InvoiceNo = reader["InvoiceNo"] != DBNull.Value ? reader["InvoiceNo"].ToString() : null,
                        BillDate = reader["BillDate"] != DBNull.Value ? Convert.ToDateTime(reader["BillDate"]) : (DateTime?)null,
                        Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : null,
                        OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : (int?)null,
                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"]) : (double?)null,
                        CGST = reader["CGST"] != DBNull.Value ? Convert.ToDouble(reader["CGST"]) : (double?)null,
                        SGST = reader["SGST"] != DBNull.Value ? Convert.ToDouble(reader["SGST"]) : (double?)null,
                        TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDouble(reader["TotalAmount"]) : (double?)null,
                        CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : null,
                        CompanyNo = reader["CompanyNo"] != DBNull.Value ? reader["CompanyNo"].ToString() : null,
                        AdminName = reader["AdminName"] != DBNull.Value ? reader["AdminName"].ToString() : null

                    };
                }
            }
            return bill;
        }
        #endregion

        #region Select By Order ID
        public BillModel SelectByOrderID(int OrderID)
        {
            BillModel bill = new BillModel();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bill_SelectByOrderID", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderID",OrderID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bill = new BillModel()
                    {
                        BillID = reader["BillID"] != DBNull.Value ? Convert.ToInt32(reader["BillID"]) : (int?)null,
                        InvoiceNo = reader["InvoiceNo"] != DBNull.Value ? reader["InvoiceNo"].ToString() : null,
                        BillDate = reader["BillDate"] != DBNull.Value ? Convert.ToDateTime(reader["BillDate"]) : (DateTime?)null,
                        Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : null,
                        OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : (int?)null,
                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"]) : (double?)null,
                        CGST = reader["CGST"] != DBNull.Value ? Convert.ToDouble(reader["CGST"]) : (double?)null,
                        SGST = reader["SGST"] != DBNull.Value ? Convert.ToDouble(reader["SGST"]) : (double?)null,
                        TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDouble(reader["TotalAmount"]) : (double?)null,
                        CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : null,
                        CompanyNo = reader["CompanyNo"] != DBNull.Value ? reader["CompanyNo"].ToString() : null
                    };
                }
            }
            return bill;
        }
        #endregion

        #region Select by Customer
        public IEnumerable<BillModel> SelectByCustomer(int CustomerID, int AdminID)
        {
            var bills = new List<BillModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bill_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@AdminID", AdminID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bills.Add(new BillModel()
                    {
                        BillID = reader["BillID"] != DBNull.Value ? Convert.ToInt32(reader["BillID"]) : (int?)null,
                        InvoiceNo = reader["InvoiceNo"] != DBNull.Value ? reader["InvoiceNo"].ToString() : null,
                        BillDate = reader["BillDate"] != DBNull.Value ? Convert.ToDateTime(reader["BillDate"]) : (DateTime?)null,
                        Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : null,
                        OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : (int?)null,
                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"]) : (double?)null,
                        CGST = reader["CGST"] != DBNull.Value ? Convert.ToDouble(reader["CGST"]) : (double?)null,
                        SGST = reader["SGST"] != DBNull.Value ? Convert.ToDouble(reader["SGST"]) : (double?)null,
                        TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDouble(reader["TotalAmount"]) : (double?)null,
                        CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : null,
                        CompanyNo = reader["CompanyNo"] != DBNull.Value ? reader["CompanyNo"].ToString() : null
                    });
                }
            }
            return bills;
        }
        #endregion

        #region Update Status
        public bool UpdateStatus(int BillID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bill_UpdateStatus", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BillID", BillID);
                conn.Open();
                
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Insert
        public int Insert(int OrderID, int AdminID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bill_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AdminID", AdminID);
                cmd.Parameters.AddWithValue("@OrderID", OrderID);

                SqlParameter outputParam = new SqlParameter
                {
                    ParameterName = "@BillID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);
                conn.Open();

                cmd.ExecuteNonQuery();

                int BillID = (int)outputParam.Value;
                return BillID;
            }
        }
        #endregion
    }
}
