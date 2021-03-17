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
    public class PersonDL
    {
        public static IEnumerable<Person> GetAllPersons()
        {
            var persons = new List<Person>();
            Thread.Sleep(700);

            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    string queryString = @"SELECT p.PersonId, FirstName, LastName, p.ModifiedDate
                                         FROM dbo.Person p 
                                         LEFT JOIN dbo.Customer c ON p.PersonId = c.PersonId
                                         WHERE c.PersonId IS NULL
                                         ORDER BY ModifiedDate DESC";

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();

                    var sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        Person person = null;
                        while (sqlDataReader.Read())
                        {
                            person = new Person();
                            person.PersonId = sqlDataReader.GetInt64(0);
                            person.FirstName = sqlDataReader.GetString(1);
                            person.LastName = sqlDataReader.GetString(2);
                            person.ModifiedDate = sqlDataReader.GetDateTime(3);

                            persons.Add(person);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return persons;
        }

        public static void AddPerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    string queryString = $"INSERT INTO dbo.Person (FirstName,LastName,ModifiedDate) VALUES(@FirstName, @LastName, @ModifiedDate)";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = person.FirstName;
                        command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = person.LastName;
                        command.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = person.ModifiedDate;

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

        public static void UpdatePerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    string queryString = @"UPDATE dbo.Person
                                           SET FirstName = @FirstName, LastName = @LastName
                                           WHERE PersonId = @PersonId";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = person.FirstName;
                        command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = person.LastName;
                        command.Parameters.Add("@PersonId", SqlDbType.BigInt).Value = person.PersonId;

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

        public static void DeletePerson(long id)
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                try
                {
                    string queryString = @"DELETE FROM dbo.Person WHERE PersonId = @id";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
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
