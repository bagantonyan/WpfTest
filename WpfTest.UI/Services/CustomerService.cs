using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WpfTest.Models.Models;

namespace WpfTest.UI.Services
{
    public class CustomerService
    {
        public async Task<List<Customer>> GetAllCustomers()
        {
            List<Customer> customers = null;
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.GetAsync("/api/Customers");
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            }
            return customers;
        }

        public async Task<List<Person>> GetAllPersons()
        {
            List<Person> persons = null;
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.GetAsync("/api/Customers/GetAllPersons");
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                persons = JsonConvert.DeserializeObject<List<Person>>(json);
            }
            return persons;
        }

        public async Task AddCustomer(string newCusstomer)
        {
            var customer = JsonConvert.DeserializeObject<Customer>(newCusstomer);
            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.PostAsync("/api/Customers", content);
        }

        public async Task DeleteCustomer(long id)
        {
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.DeleteAsync("/api/Customers/" + id);
        }

        public async Task UpdateCustomer(string updatedCustomer)
        {
            var customer = JsonConvert.DeserializeObject<Customer>(updatedCustomer);
            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.PutAsync("/api/Customers", content);
        }
    }
}
