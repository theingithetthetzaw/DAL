using Data.Models;
using Infra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DALWeb.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserForm(int ID = 0, string FormType = "Add")
        {
            ViewBag.ID = ID;
            ViewBag.FormType = FormType;
            return View();
        }

        public async Task<ActionResult> _personList(string FilterName = null)
        {
            List<tbCustomer> result = await PersonRequestHelper.getPerson(FilterName);
            return PartialView("_personList", result);
        }

        public async Task<ActionResult> _UserForm(string FormType = null, int ID = 0)
        {
            tbCustomer Person = new tbCustomer();

            if (FormType == "Add")
            {
                return PartialView("_UserForm", Person);
            }
            else
            {
                tbCustomer result = await PersonRequestHelper.Get(ID);
                return PartialView("_UserForm", result);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpsertPerson(tbCustomer customer)

        {
            tbCustomer result = await PersonRequestHelper.Upsert(customer);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(int ID)
        {
            var url = string.Format("api/Person/DeletePerson?ID={0}", ID);
            tbCustomer result = await ApiRequest<tbCustomer>.GetRequest(url);
            if (result != null)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }


    }
}