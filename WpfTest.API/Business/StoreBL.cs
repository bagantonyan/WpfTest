using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfTest.API.Data;
using WpfTest.Models.Models;

namespace WpfTest.API.Business
{
    public class StoreBL
    {
        public static IEnumerable<Store> GetAllStores()
        {
            return StoreDL.GetAllStores();
        }

        public static Store GetStoreById(long id)
        {
            return StoreDL.GetStoreById(id);
        }

        public static void AddStore(Store store)
        {
            StoreDL.AddStore(store);
        }

        public static void UpdateStore(Store store)
        {
            StoreDL.UpdateStore(store);
        }

        public static void DeleteStore(long id)
        {
            StoreDL.DeleteStore(id);
        }
    }
}
