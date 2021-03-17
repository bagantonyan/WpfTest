using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WpfTest.Models.Models;

namespace WpfTest.API.Data
{
    public class CustomerDL
    {
        public static IEnumerable<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();
            Thread.Sleep(700);

            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    string queryString = @"SELECT c.CustomerId, p.FirstName, p.LastName, s.Name, c.ModifiedDate, c.StoreId, c.PersonId
                                         FROM dbo.Customer c
                                         INNER JOIN dbo.Person p ON c.PersonId = p.PersonId
                                         INNER JOIN dbo.Store s ON c.StoreId = s.StoreId
                                         ORDER BY ModifiedDate DESC";

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();

                    var sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        Customer customer = null;
                        while (sqlDataReader.Read())
                        {
                            customer = new Customer();
                            customer.CustomerId = sqlDataReader.GetInt64(0);
                            customer.FirstName = sqlDataReader.GetString(1);
                            customer.LastName = sqlDataReader.GetString(2);
                            customer.StoreName = sqlDataReader.GetString(3);
                            customer.ModifiedDate = sqlDataReader.GetDateTime(4);
                            customer.StoreId = sqlDataReader.GetInt64(5);
                            customer.PersonId = sqlDataReader.GetInt64(6);

                            customers.Add(customer);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return customers;
        }

        public static void AddCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    string queryString = @"INSERT INTO dbo.Customer (PersonId,StoreId,ModifiedDate) 
                                                             VALUES(@PersonId,@StoreId,GETDATE())";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.Add("@PersonId", SqlDbType.BigInt).Value = customer.PersonId;
                        command.Parameters.Add("@StoreId", SqlDbType.BigInt).Value = customer.StoreId;

                        command.Connection.Open();
                        command.ExecuteNonQueryAsync();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public static void UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                connection.Open();
                SqlTransaction sqlTran = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = sqlTran;
                        command.CommandText = @"UPDATE dbo.Person
                                                SET FirstName = @FirstName, LastName = @LastName
                                                WHERE PersonId = @PersonId
                                                UPDATE dbo.Customer
                                                SET StoreId = @StoreId
                                                WHERE PersonId = @PersonId";
                        command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = customer.FirstName;
                        command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = customer.LastName;
                        command.Parameters.Add("@PersonId", SqlDbType.BigInt).Value = customer.PersonId;
                        command.Parameters.Add("@StoreId", SqlDbType.BigInt).Value = customer.StoreId;
                        command.ExecuteNonQuery();
                        sqlTran.Commit();
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        sqlTran.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        throw exRollback;
                    }
                }
            }
        }

        public static void DeleteCustomer(long id)
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    string queryString = $"DELETE FROM dbo.Customer WHERE CustomerId = {id}";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
    }
}
