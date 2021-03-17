using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using WpfTest.Models.Models;
using System.IO;
using System.Data;
using WpfTest.API.Business;

namespace WpfTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Store> GetAllStores()
        {
            return StoreBL.GetAllStores();
        }

        [HttpGet("{id}", Name = "GetStore")]
        public Store GetStoreById(long id)
        {
            return StoreBL.GetStoreById(id);
        }

        [HttpPost]
        public void AddStore(Store store)
        {
            StoreBL.AddStore(store);
        }

        [HttpPut]
        public void UpdateStore(Store store)
        {
            StoreBL.UpdateStore(store);
        }

        [HttpDelete("{id}")]
        public void DeleteStore(long id)
        {
            StoreBL.DeleteStore(id);
        }
    }
}
