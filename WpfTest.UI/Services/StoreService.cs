using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using WpfTest.Models.Models;
using System.Net.Http.Headers;

namespace WpfTest.UI.Services
{
    public class StoreService
    {
        public async Task<List<Store>> GetAllStores()
        {
            List<Store> stores = null;
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.GetAsync("/api/Stores");    //("http://localhost:1099/api/Stores");
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                stores = JsonConvert.DeserializeObject<List<Store>>(json);
            }
            return stores;
        }

        public async Task AddStore(string newStore)
        {
            var store = JsonConvert.DeserializeObject<Store>(newStore);
            var content = new StringContent(JsonConvert.SerializeObject(store), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.PostAsync("/api/Stores", content);
        }

        public async Task DeleteStore(long id)
        {
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.DeleteAsync("/api/Stores/" + id);
        }

        public async Task UpdateStore(string updatedStore)
        {
            var store = JsonConvert.DeserializeObject<Store>(updatedStore);
            var content = new StringContent(JsonConvert.SerializeObject(store), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.PutAsync("/api/Stores", content);
        }

        public async Task<Store> GetStoreById(long id)
        {
            Store store = null;
            HttpResponseMessage httpResponse = await SingletonHttpClient.HttpClient.GetAsync($"/api/Stores/{id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                store = JsonConvert.DeserializeObject<Store>(json);
            }
            return store;
        }
    }
}
