using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Data
{
    public class CustomerDB
    {
        private string connectionString = "Data Source=LAB1504-01\\SQLEXPRESS;Initial Catalog=FacturaDB;User ID=tecsup;Password=tecsup";
        public List<Customer> ListCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("ListCustomers", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer()
                            {
                                Id = Convert.ToInt32(reader["customer_id"]),
                                Name = reader["name"].ToString(),
                                Address = reader["address"].ToString(),
                                Phone = reader["phone"].ToString(),
                            };

                            customers.Add(customer);
                        }
                    }
                }
            }

            return customers;
        }
        public List<Customer> GetCustomer()
        {
            CustomerDB customerDataAccess = new CustomerDB();
            return customerDataAccess.ListCustomers();
        }


        public void InsertCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("InsertCustomer", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@name", customer.Name));
                    cmd.Parameters.Add(new SqlParameter("@address", customer.Address));
                    cmd.Parameters.Add(new SqlParameter("@phone", customer.Phone));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateCustomer", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@customer_id", customer.Id));
                    cmd.Parameters.Add(new SqlParameter("@name", customer.Name));
                    cmd.Parameters.Add(new SqlParameter("@address", customer.Address));
                    cmd.Parameters.Add(new SqlParameter("@phone", customer.Phone));
                    cmd.Parameters.Add(new SqlParameter("@active", customer.Active));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteCustomer(int customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteCustomer", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@customer_id", customer);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}