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
    public class PersonsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Person> GetAllPersons()
        {
            return PersonBL.GetAllPersons();
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            PersonBL.AddPerson(person);
        }

        [HttpPut]
        public void UpdatePerson(Person person)
        {
            PersonBL.UpdatePerson(person);
        }

        [HttpDelete("{id}")]
        public void DeletePerson(long id)
        {
            PersonBL.DeletePerson(id);
        }

        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
