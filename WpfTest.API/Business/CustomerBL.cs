using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfTest.API.Data;
using WpfTest.Models.Models;

namespace WpfTest.API.Business
{
    public class CustomerBL
    {
        public static IEnumerable<Customer> GetAllCustomers()
        {
            return CustomerDL.GetAllCustomers();
        }

        public static void AddCustomer(Customer customer)
        {
            CustomerDL.AddCustomer(customer);
        }

        public static void UpdateCustomer(Customer customer)
        {
            CustomerDL.UpdateCustomer(customer);
        }

        public static void DeleteCustomer(long id)
        {
            CustomerDL.DeleteCustomer(id);
        }
    }
}
