using Factory_Management_System_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Factory_Management_System_WebAPI.Data
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Select All Product
        public IEnumerable<ProductModel> SelectAll(int AdminID, double? MaxPrice, double? MinPrice , string? ProductName)
        {
            var products = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@MaxPrice", MaxPrice);
                cmd.Parameters.AddWithValue("@MinPrice", MinPrice);
                cmd.Parameters.AddWithValue("@ProductName", ProductName);
                cmd.Parameters.AddWithValue("@AdminID",AdminID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new ProductModel()
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        Material = reader["Material"].ToString(),
                        Price = Convert.ToDouble(reader["Price"]),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    });
                }
            }
            return products;
        }
        #endregion

        #region Select By PK
        public ProductModel SelectByPK(int ProductID)
        {
            ProductModel product = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_SelectByPK", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new ProductModel()
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        Material = reader["Material"].ToString(),
                        Price = Convert.ToDouble(reader["Price"]),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
            }
            return product;
        }
        #endregion

        #region Insert
        public bool Insert(ProductModel product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AdminID",product.AdminID);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Material", product.Material);
                conn.Open();
                
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update Price
        public bool UpdatePrice(int ProductID, double Price)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_UpdatePrice", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(ProductModel product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_Update", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Material", product.Material);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Delete
        public bool Delete(int ProductID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                conn.Open();

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Product DropDown
        public IEnumerable<ProductDropDownModel> ProductDropDown(int OrderID)
        {
            var products = new List<ProductDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_DropDown", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new ProductDropDownModel()
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString()
                    });
                }
            }
            return products;
        }
        #endregion
    }
}
