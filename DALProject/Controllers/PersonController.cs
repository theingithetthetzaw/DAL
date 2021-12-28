using DALProject.Repository;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace DALProject.Controllers
{
    public class PersonController : ApiController
    {
        DBContext dbContext;
        private PersonRepository personRepo = null;

        public PersonController()
        {
            
            dbContext = new DBContext();

            personRepo = new PersonRepository(dbContext);
            
        }



        //[HttpGet]
        //[Route("api/Person/GetPersonByID")]
        //public HttpResponseMessage GetPersonByID(HttpRequestMessage request, int ID = 0)
        //{
        //    List<tbPerson> result;
        //    result = personRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.PersonID == ID).ToList();
        //    return request.CreateResponse(HttpStatusCode.OK, result);
        //}

        [HttpGet]
        [Route("api/Person/GetPersonByID")]
        public HttpResponseMessage GetPersonByID(HttpRequestMessage request, int ID = 0)
        {
            tbCustomer result;
            result = personRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.PersonID == ID).FirstOrDefault();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        //[HttpGet]
        //[Route("api/ShowRoom/GetShowRoomByID")]
        //public HttpResponseMessage GetShowRoomByID(HttpRequestMessage request, int ID = 0)
        //{
        //    tbShowroom result = new tbShowroom();
        //    result = showroomRepo.GetAll().Where(a => a.IsDeleted != true && a.ID == ID).FirstOrDefault();
        //    return request.CreateResponse(HttpStatusCode.OK, result);
        //}

        [HttpGet]
        [Route("api/Person/GetPersonList")]
        public HttpResponseMessage GetPersonList(HttpRequestMessage request, string FilterName = null)
        {
            //IQueryable<tbPerson> result;
            //result = personRepo.GetWithoutTracking().Where(a => a.IsDeleted != true).AsQueryable();
            //return request.CreateResponse(HttpStatusCode.OK, result);

            List<tbCustomer> result = new List<tbCustomer>();
            Expression<Func<tbCustomer, bool>> namefilter = null;
            if (FilterName != null)
            {
                namefilter = l => l.Name.Contains(FilterName);
            }
            else
            {
                namefilter = l => l.IsDeleted != true;
            }

            result = personRepo.Get().Where(a => a.IsDeleted != true).Where(namefilter).ToList();

            return request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpPost]
        [Route("api/Person/UpsertPerson")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> UpsertPerson(HttpRequestMessage request, tbCustomer c) 
        {
            

            tbCustomer UpdatedEntity = null;
           
           
            if (c.PersonID > 0)

            {               
               
               tbCustomer presult = dbContext.tbcustomer.FirstOrDefault(a => a.IsDeleted != true && a.Email == c.Email);
                presult.Name = c.Name;
                
                presult.IsDeleted = false;
                presult.Phone = c.Phone;
                presult.Fax = c.Fax;
                presult.Postcode = c.Postcode;
                presult.Country = c.Country;
                presult.Address = c.Address;
                presult.Accesstime = Extensions.MyExtension.getLocalTime(DateTime.UtcNow);

                UpdatedEntity = personRepo.UpdatewithObj(presult);

            }
            else
            {
                
                c.IsDeleted = false;
                c.Accesstime = Extensions.MyExtension.getLocalTime(DateTime.UtcNow);
                


                UpdatedEntity = personRepo.AddWithGetObj(c);

              
                
               
               
            }
           

            return request.CreateResponse(HttpStatusCode.OK, UpdatedEntity);

        }

        [HttpGet]
        [Route("api/Person/DeletePerson")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> DeletePerson(HttpRequestMessage request, int ID = 0) 
        {
            tbCustomer sr = new tbCustomer();

            tbCustomer UpdatedEntity = null;
            tbCustomer aa = personRepo.GetWithoutTracking().Where(a => a.IsDeleted != true && a.PersonID == ID).FirstOrDefault();
            if (aa != null)
            {
                aa.IsDeleted = true;
                personRepo.Update(aa);
                return request.CreateResponse(HttpStatusCode.OK, aa);
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.OK, UpdatedEntity);
            }

        }
    }
}