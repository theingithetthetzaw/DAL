using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class PersonRequestHelper
    {
        public static async Task<tbCustomer> Get(int ID)
        {
            string url = string.Format("api/Person/GetPersonByID?ID={0}", ID);
            tbCustomer result = await ApiRequest<tbCustomer>.GetRequest(url);
            return result;
        }

        public static async Task<List<tbCustomer>> getPerson(string Filtername= null)
        {

           
            string url = string.Format("api/Person/GetPersonList?FilterName={0}", Filtername);
            var data = await ApiRequest<List<tbCustomer>>.GetRequest(url);

            return data;

        }

        public static async Task<tbCustomer> Upsert(tbCustomer customer)
        {
            var url = "api/Person/UpsertPerson";
            tbCustomer result = await ApiRequest<tbCustomer>.PostRequest(url, customer);
            return result;
        }
    }
}
