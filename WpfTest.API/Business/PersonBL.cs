using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfTest.API.Data;
using WpfTest.Models.Models;

namespace WpfTest.API.Business
{
    public class PersonBL
    {
        public static IEnumerable<Person> GetAllPersons()
        {
            return PersonDL.GetAllPersons();
        }

        public static void AddPerson(Person person)
        {
            PersonDL.AddPerson(person);
        }

        public static void UpdatePerson(Person person)
        {
            PersonDL.UpdatePerson(person);
        }

        public static void DeletePerson(long id)
        {
            PersonDL.DeletePerson(id);
        }
    }
}
