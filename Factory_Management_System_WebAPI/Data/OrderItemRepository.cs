using Factory_Management_System_WebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace Factory_Management_System_WebAPI.Data
{
    public class OrderItemRepository
    {
        private readonly string _connectionString;
        public OrderItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Select All
        public IEnumerable<OrderItemModel> SelectAll(int AdminID)
        {
            var orderItems =  new List<OrderItemModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderItem_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AdminID",AdminID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orderItems.Add(new OrderItemModel()
                    {
                        OrderItemID = reader["OrderItemID"] != DBNull.Value ? Convert.ToInt32(reader["OrderItemID"]) : 0, // Default to 0 if null
                        OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : 0, // Default to 0 if null
                        ProductID = reader["ProductID"] != DBNull.Value ? Convert.ToInt32(reader["ProductID"]) : 0, // Default to 0 if null
                        Quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0, // Default to 0 if null
                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToInt32(reader["Amount"]) : 0, // Default to 0 if null
                        ProductPrice = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0, // Default to 0 if null
                        ProductName = reader["ProductName"] != DBNull.Value ? reader["ProductName"].ToString() : string.Empty, // Default to empty string if null
                        Material = reader["Material"] != DBNull.Value ? reader["Material"].ToString() : string.Empty // Default to empty string if null

                    });
                }
            }
            return orderItems;
        }
        #endregion

        #region Select By PK
        public OrderItemModel SelectByPK(int OrderItemID)
        {
            OrderItemModel orderItem = new OrderItemModel();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderItem_SelectByPK", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderItemID",OrderItemID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    orderItem = new OrderItemModel()
                    {
                        OrderItemID = reader["OrderItemID"] != DBNull.Value ? Convert.ToInt32(reader["OrderItemID"]) : 0, // Default to 0 if null
                        OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : 0, // Default to 0 if null
                        ProductID = reader["ProductID"] != DBNull.Value ? Convert.ToInt32(reader["ProductID"]) : 0, // Default to 0 if null
                        Quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0, // Default to 0 if null
                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToInt32(reader["Amount"]) : 0, // Default to 0 if null
                        ProductPrice = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0, // Default to 0 if null
                        ProductName = reader["ProductName"] != DBNull.Value ? reader["ProductName"].ToString() : string.Empty, // Default to empty string if null
                        Material = reader["Material"] != DBNull.Value ? reader["Material"].ToString() : string.Empty // Default to empty string if null

                    };
                }
            }
            return orderItem;
        }
        #endregion

        #region Select By OrderID
        public IEnumerable<OrderItemModel> SelectByOrderID(int OrderID)
        {
            var orderItems = new List<OrderItemModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderItem_SelectByOrderID", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orderItems.Add(new OrderItemModel()
                    {
                        OrderItemID = reader["OrderItemID"] != DBNull.Value ? Convert.ToInt32(reader["OrderItemID"]) : 0, // Default to 0 if null
                        OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : 0, // Default to 0 if null
                        ProductID = reader["ProductID"] != DBNull.Value ? Convert.ToInt32(reader["ProductID"]) : 0, // Default to 0 if null
                        Quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0, // Default to 0 if null
                        Amount = reader["Amount"] != DBNull.Value ? Convert.ToInt32(reader["Amount"]) : 0, // Default to 0 if null
                        ProductPrice = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0, // Default to 0 if null
                        ProductName = reader["ProductName"] != DBNull.Value ? reader["ProductName"].ToString() : string.Empty, // Default to empty string if null
                        Material = reader["Material"] != DBNull.Value ? reader["Material"].ToString() : string.Empty // Default to empty string if null

                    });
                }
            }
            return orderItems;
        }
        #endregion

        #region Insert
        public int Insert(OrderItemAddModel orderItemModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderItem_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                Console.WriteLine(orderItemModel.AdminID);

                cmd.Parameters.AddWithValue("@AdminID", orderItemModel.AdminID);
                cmd.Parameters.AddWithValue("@ProductID", orderItemModel.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", orderItemModel.Quantity);
                cmd.Parameters.AddWithValue("@OrderID", orderItemModel.OrderID);   
                SqlParameter outputParam = new SqlParameter
                {
                    ParameterName = "@OrderItemID",
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

        #region Update OrderID
        public bool UpdateOrderID(int OrderID, int OrderItemID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderItem_UpdateOrderID", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };


                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                cmd.Parameters.AddWithValue("@OrderItemID", OrderItemID);


                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update Order Item
        public bool Update(OrderItemModel orderItem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderItem_Update", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Quantity",orderItem.Quantity);
                cmd.Parameters.AddWithValue("@OrderID", orderItem.OrderID);
                cmd.Parameters.AddWithValue("@OrderItemID", orderItem.OrderItemID);
                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Delete
        public bool Delete(int OrderItemID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderItem_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderItemID", OrderItemID);
                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Delete By OrderID
        public bool DeleteByOrderID(int OrderID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderItem_Delete_OrderID", conn)
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
    }
}
