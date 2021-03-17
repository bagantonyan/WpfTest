using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WpfTest.Models.Models;

namespace WpfTest.API.Data
{
    public class StoreDL
    {
        public static IEnumerable<Store> GetAllStores()
        {
            var stores = new List<Store>();
            Thread.Sleep(700);
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    string queryString = "SELECT * FROM dbo.Store ORDER BY ModifiedDate DESC";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Connection.Open();

                        var sqlDataReader = command.ExecuteReader();
                        if (sqlDataReader.HasRows)
                        {
                            Store store = null;
                            while (sqlDataReader.Read())
                            {
                                store = new Store();
                                store.StoreId = sqlDataReader.GetInt64(0);
                                store.Name = sqlDataReader.GetString(1);
                                store.Address = sqlDataReader.GetString(2);
                                store.ModifiedDate = sqlDataReader.GetDateTime(3);

                                stores.Add(store);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return stores;
        }

        public static Store GetStoreById(long id)
        {
            var store = new Store();

            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    string queryString = "SELECT * FROM dbo.Store WHERE StoreId = @StoreId";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.Add("@StoreId", SqlDbType.BigInt).Value = id;
                        command.Connection.Open();

                        var sqlDataReader = command.ExecuteReader();
                        if (sqlDataReader.Read())
                        {
                            store.StoreId = sqlDataReader.GetInt64(0);
                            store.Name = sqlDataReader.GetString(1);
                            store.Address = sqlDataReader.GetString(2);
                            store.ModifiedDate = sqlDataReader.GetDateTime(3);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return store;
        }

        public static void AddStore(Store store)
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    string queryString = $"INSERT INTO dbo.Store (Name,Address,ModifiedDate) VALUES(@Name, @Address, @ModifiedDate)";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = store.Name;
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = store.Address;
                        command.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = store.ModifiedDate;

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

        public static void UpdateStore(Store store)
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    string queryString = @"UPDATE dbo.Store
                                           SET Name = @Name, Address = @Address
                                           WHERE StoreId = @StoreId";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = store.Name;
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = store.Address;
                        command.Parameters.Add("@StoreId", SqlDbType.BigInt).Value = store.StoreId;

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
        public static void DeleteStore(long id)
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
                        command.CommandText = @"DELETE FROM dbo.Store WHERE StoreId = @id";
                        command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                        command.ExecuteNonQuery();

                        command.CommandText = @"DELETE FROM dbo.Customer 
                                                WHERE StoreId NOT IN (SELECT s.StoreId FROM dbo.Store s)";
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
    }
}
