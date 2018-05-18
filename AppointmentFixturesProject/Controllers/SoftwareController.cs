using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;
using BLL;

namespace AppointmentFixturesProject.Controllers
{

     [Authorize(Roles = "Master")]
    public class SoftwareController : Controller
    {
        BLLCompany blcompany = new BLLCompany();


        public SoftwareController()
        {

        }


        // GET: Software
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCompany(BOCompany model)
        {
            blcompany.CreateCompany(model);
            return View();
        }

        public ActionResult ViewCompany()
        {
            return View(blcompany.GetAllCompany());
        }

        public ActionResult EditCompany(int id)
        {
            BOCompany company = blcompany.GetCompanyById(id);
            if (company.Photo == null) { company.Photo = "default.png"; }
            return View(company);
        }

         [HttpPost]
        public ActionResult EditCompany(BOCompany model)
        {
            blcompany.UpdateCompany(model);
            return RedirectToAction("ViewCOmpany");
        }

         public ActionResult DeleteCompany(int id)
         {
             BOCompany company = blcompany.GetCompanyById(id);
             return View(company);
         }

         [HttpPost]
         public ActionResult DeleteCompany(BOCompany model)
         {
             blcompany.DeleteCompany(model.Id);
             return RedirectToAction("ViewCOmpany");
         }

         public ActionResult DetailsCompany(int id)
         {
             BOCompany company = blcompany.GetCompanyById(id);
             return View(company);
         }

         [HttpPost]
         public ActionResult DetailsCompany(BOCompany model)
         {

             return View();
         }
    
    }
}