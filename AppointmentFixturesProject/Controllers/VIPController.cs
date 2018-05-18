using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;
using BLL;
using Microsoft.AspNet.Identity;

namespace AppointmentFixturesProject.Controllers
{
    [Authorize(Roles = "COMPANYVIP")]
    public class VIPController : Controller
    {
        public static int VIPID = 0;
        BLLAvailableTiming blavailable = new BLLAvailableTiming();
        BLLVIP blvip = new BLLVIP();
        BLLCompany bllCompany = new BLLCompany();
        BLLAppointmentDetails bllAppointment = new BLLAppointmentDetails();
        BLLDateTime bllDateTime = new BLLDateTime();

        // GET: VIP
        BLLAvailableTiming available = new BLLAvailableTiming();


        public VIPController()
        {
            string id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var vip = blvip.GetAllVIP().Where(u => u.UserId == id).FirstOrDefault();
            VIPID = vip.Id;
        }

        public ActionResult Index()
        {
            BOVIPTable vip = blvip.GetVIPById(VIPID);
            return View(vip);
           
        }

        [HttpPost]
        public ActionResult Index(BOVIPTable model)
        {
            blvip.UpdateVIP(model);
            return View(model);
        }

        public ActionResult FixAppointment()
        {
            return View();
        }

      

        //Notification Message
        BLLAppointmentDetails bllappointmentdetails = new BLLAppointmentDetails();
        [HttpGet]
        public JsonResult GetNotifications()
        {
            return Json(bllappointmentdetails.GetAllAppointment(), JsonRequestBehavior.AllowGet);
        }

        //diwas part

        public JsonResult List()
        {
            var appointmentLst = blavailable.GetAvailableTimingByVIP(VIPID);
            return Json(appointmentLst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(BOAvailableTiming model)
        {
            model.VipId = VIPID;
            int i = blavailable.AddAvailableTiming(model);
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(BOAvailableTiming model)
        {
            model.VipId = VIPID;
            var appointment = blavailable.UpdateAvailableTiming(model);
            return Json(appointment, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int Id)
        {
            var appoint = blavailable.GetIndividualAvailableTiming(Id);
            return Json(appoint, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            var temp = blavailable.DeleteAvailableTimings(ID);
            return Json(temp, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public ActionResult FixAppointment(BOAvailableTiming model)
        {
            if (ModelState.IsValid)
            {
                model.VipId = VIPID;
                blavailable.AddAvailableTiming(model);

            }
            return View();

        }


        //public ActionResult ViewAppointment()
        //{
        //    string email = System.Web.HttpContext.Current.User.Identity.GetUserName();
        //    var temp = blavailable.getBookAppointmentByUser(email);


        //    return View(temp);
        //}


        public ActionResult UpdatingAppointment(int id)
        {
            var temp = bllDateTime.GetAllDateTime();
            var bDateTime = temp.Where(u => u.Id == id).SingleOrDefault();
            return View(bDateTime);
        }

        [HttpPost]
        public ActionResult UpdatingAppointment(BODateTime bDateTime)
        {
            if (ModelState.IsValid)
            {
                int i = bllDateTime.UpdateDateTime(bDateTime);
                if (i > 0)
                {
                    return RedirectToAction("ViewAppointment");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }



    }
}