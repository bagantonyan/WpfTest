using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WpfTest.API.Business;
using WpfTest.Models.Models;

namespace WpfTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return CustomerBL.GetAllCustomers();
        }

        [HttpPost]
        public void AddCustomer(Customer customer)
        {
            CustomerBL.AddCustomer(customer);
        }

        [HttpPut]
        public void UpdateCustomer(Customer customer)
        {
            CustomerBL.UpdateCustomer(customer);
        }

        [HttpDelete("{id}")]
        public void DeleteCustomer(long id)
        {
            CustomerBL.DeleteCustomer(id);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
