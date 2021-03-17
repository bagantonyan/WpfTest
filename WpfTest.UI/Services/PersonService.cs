using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfTest.Models.Models;

namespace WpfTest.UI.Services
{
    public class PersonService
    {
        public async Task<List<Person>> GetAllPersons()
        {
            List<Person> persons = null;
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.GetAsync("/api/Persons");    //("http://localhost:1099/api/Stores");
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                persons = JsonConvert.DeserializeObject<List<Person>>(json);
            }
            return persons;
        }

        public async void AddPerson(string newPerson)
        {
            var person = JsonConvert.DeserializeObject<Person>(newPerson);
            var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.PostAsync("/api/Persons", content);
        }

        public async Task DeletePerson(long id)
        {
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.DeleteAsync("/api/Persons/" + id);
        }

        public async Task UpdatePerson(string updatedPerson)
        {
            var person = JsonConvert.DeserializeObject<Person>(updatedPerson);
            var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.PutAsync("/api/Persons", content);
        }
    }
}
