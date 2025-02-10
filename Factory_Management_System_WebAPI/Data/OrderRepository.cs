using Factory_Management_System_WebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace Factory_Management_System_WebAPI.Data
{
    public class OrderRepository
    {
        private readonly string _connectionString;
        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Select All
        public IEnumerable<OrderModel> SelectAll(int AdminID, string? CustomerName, string? status, double? MinValue, double? MaxValue, DateTime? StartDate, DateTime? EndDate)
        {
            var orders = new List<OrderModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@MinValue", MinValue);
                cmd.Parameters.AddWithValue("@MaxValue", MaxValue);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@AdminID", AdminID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orders.Add(new OrderModel()
                    {
                        CustomerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0,
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : string.Empty,
                        OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : 0,
                        Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : string.Empty,
                        RequestStatus = reader["RequestStatus"] != DBNull.Value ? reader["RequestStatus"].ToString() : string.Empty,
                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"]) : 0.0,
                        CGST = reader["CGST"] != DBNull.Value ? Convert.ToDouble(reader["CGST"]) : 0.0,
                        SGST = reader["SGST"] != DBNull.Value ? Convert.ToDouble(reader["SGST"]) : 0.0,
                        TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDouble(reader["TotalAmount"]) : 0.0,
                        RequestedDate = reader["RequestedDate"] != DBNull.Value ? Convert.ToDateTime(reader["RequestedDate"]) : (DateTime?)null,
                        CompletedDate = reader["CompletedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CompletedDate"]) : (DateTime?)null,
                        ApprovedDate = reader["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovedDate"]) : (DateTime?)null,
                        //AdminID = reader["AdminID"] != DBNull.Value ? Convert.ToInt32(reader["AdminID"]) : 0
                        ItemCount = reader["ItemCount"] != DBNull.Value ? Convert.ToInt32(reader["ItemCount"]) : 0,
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? Convert.ToDateTime(reader["DeliveredDate"]) : (DateTime?)null
                    });
                }
            }
            return orders;
        }
        #endregion

        #region Select Customer All Order
        public IEnumerable<OrderModel> SelectByCustomerID(int CustomerID, int AdminID)
        {
            var orders = new List<OrderModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AdminID", AdminID);

                cmd.Parameters.AddWithValue("@CustomerID",CustomerID);
                Console.WriteLine("CustomerID = " + CustomerID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orders.Add(new OrderModel()
                    {
                        CustomerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0,
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : string.Empty,
                        OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : 0,
                        Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : string.Empty,
                        RequestStatus = reader["RequestStatus"] != DBNull.Value ? reader["RequestStatus"].ToString() : string.Empty,
                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"]) : 0.0,
                        CGST = reader["CGST"] != DBNull.Value ? Convert.ToDouble(reader["CGST"]) : 0.0,
                        SGST = reader["SGST"] != DBNull.Value ? Convert.ToDouble(reader["SGST"]) : 0.0,
                        TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDouble(reader["TotalAmount"]) : 0.0,
                        RequestedDate = reader["RequestedDate"] != DBNull.Value ? Convert.ToDateTime(reader["RequestedDate"]) : (DateTime?)null,
                        CompletedDate = reader["CompletedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CompletedDate"]) : (DateTime?)null,
                        ApprovedDate = reader["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovedDate"]) : (DateTime?)null,
                        ItemCount = reader["ItemCount"] != DBNull.Value ? Convert.ToInt32(reader["ItemCount"]) : 0,
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? Convert.ToDateTime(reader["DeliveredDate"]) : (DateTime?)null
                    });
                }
            }
            return orders;
        }
        #endregion

        #region Select By PK
        public OrderModel SelectByPK(int OrderID)
        {
            OrderModel order = new OrderModel();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_SelectByPK", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    order = new OrderModel()
                    {
                        CustomerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0,
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : string.Empty,
                        OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : 0,
                        Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : string.Empty,
                        RequestStatus = reader["RequestStatus"] != DBNull.Value ? reader["RequestStatus"].ToString() : string.Empty,
                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"]) : 0.0,
                        CGST = reader["CGST"] != DBNull.Value ? Convert.ToDouble(reader["CGST"]) : 0.0,
                        SGST = reader["SGST"] != DBNull.Value ? Convert.ToDouble(reader["SGST"]) : 0.0,
                        TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDouble(reader["TotalAmount"]) : 0.0,
                        RequestedDate = reader["RequestedDate"] != DBNull.Value ? Convert.ToDateTime(reader["RequestedDate"]) : (DateTime?)null,
                        CompletedDate = reader["CompletedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CompletedDate"]) : (DateTime?)null,
                        ApprovedDate = reader["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovedDate"]) : (DateTime?)null,
                        ItemCount = reader["ItemCount"] != DBNull.Value ? Convert.ToInt32(reader["ItemCount"]) : 0,
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        DeliveredDate = reader["DeliveredDate"] != DBNull.Value ? Convert.ToDateTime(reader["DeliveredDate"]) : (DateTime?)null
                    };
                }
            }
            return order;
        }
        #endregion

        #region Update Order Status
        public int UpdateOrderStatus(int OrderID, string NewStatus, int AdminID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("PR_Order_UpdateStatus", conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@AdminID", AdminID);
                    cmd.Parameters.AddWithValue("@NewStatus", NewStatus);
                    cmd.Parameters.AddWithValue("@OrderID", OrderID);

                    // Add return value parameter
                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Get return value from SQL procedure
                    int result = (int)returnValue.Value;
                    Console.WriteLine("Procedure Return Value: " + result);

                    return result;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                    return -2; // Custom error code for SQL failure
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General Error: " + ex.Message);
                    return -3; // Custom error code for general failure
                }
            }
        }

        #endregion

        #region Insert
        public int Insert(int CustomerID, int AdminID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@AdminID", AdminID);

                SqlParameter outputParam = new SqlParameter
                {
                    ParameterName = "@OrderID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);
                conn.Open();

                cmd.ExecuteNonQuery();

                int OrderItemID = (int)outputParam.Value;
                return OrderItemID;
            }
        }
        #endregion

        #region Update Amount
        public bool UpdateAmounts(int OrderID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_UpdateAmounts", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update Request Status
        public bool UpdateRequestStatus(int OrderID, string RequestStatus)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_UpdateRequestStatus", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@RequestStatus", RequestStatus);
                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region delete order
        public bool Delete(int OrderID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Select Order Request
        public IEnumerable<OrderModel> SelectAllOrderRequest(int AdminID, string? CustomerName, string? status, double? MinValue, double? MaxValue, DateTime? StartDate, DateTime? EndDate)
        {
            var orders = new List<OrderModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderRequest_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@MinValue", MinValue);
                cmd.Parameters.AddWithValue("@MaxValue", MaxValue);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@AdminID", AdminID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orders.Add(new OrderModel()
                    {
                        CustomerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0,
                        CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : string.Empty,
                        OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : 0,
                        Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : string.Empty,
                        RequestStatus = reader["RequestStatus"] != DBNull.Value ? reader["RequestStatus"].ToString() : string.Empty,
                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"]) : 0.0,
                        CGST = reader["CGST"] != DBNull.Value ? Convert.ToDouble(reader["CGST"]) : 0.0,
                        SGST = reader["SGST"] != DBNull.Value ? Convert.ToDouble(reader["SGST"]) : 0.0,
                        TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDouble(reader["TotalAmount"]) : 0.0,
                        RequestedDate = reader["RequestedDate"] != DBNull.Value ? Convert.ToDateTime(reader["RequestedDate"]) : (DateTime?)null,
                        CompletedDate = reader["CompletedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CompletedDate"]) : (DateTime?)null,
                        ApprovedDate = reader["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovedDate"]) : (DateTime?)null,
                        //AdminID = reader["AdminID"] != DBNull.Value ? Convert.ToInt32(reader["AdminID"]) : 0
                        ItemCount = reader["ItemCount"] != DBNull.Value ? Convert.ToInt32(reader["ItemCount"]) : 0
                    });
                }
            }
            return orders;
        }
        #endregion

    }
}
