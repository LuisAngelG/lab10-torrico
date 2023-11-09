using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ProductDB
    {
        private string connectionString = "Data Source=LAB1504-01\\SQLEXPRESS;Initial Catalog=FacturaDB;User ID=tecsup;Password=tecsup";
        public List<Product> ListProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("ListProducts", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Id = Convert.ToInt32(reader["product_id"]),
                                Name = reader["name"].ToString(),
                                Price = Convert.ToDecimal(reader["price"]),
                                Stock = Convert.ToInt32(reader["stock"]),
                                Active = Convert.ToBoolean(reader["active"])
                            };

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }
        public List<Product> GetProducts()
        {
            ProductDB customerdb = new ProductDB();
            return customerdb.ListProducts();
        }

        public void UpdateProduct(Product updatedProduct)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", updatedProduct.Id);
                    command.Parameters.AddWithValue("@name", updatedProduct.Name);
                    command.Parameters.AddWithValue("@stock", updatedProduct.Stock);
                    command.Parameters.AddWithValue("@price", updatedProduct.Price);

                    int rowsAffected = command.ExecuteNonQuery();

                }
            }
        }
        public void CreateProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("InsertProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@name", product.Name));
                    cmd.Parameters.Add(new SqlParameter("@stock", product.Stock));
                    cmd.Parameters.Add(new SqlParameter("@price", product.Price));

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}