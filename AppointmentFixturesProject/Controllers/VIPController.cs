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

        public JsonResult List()
        {
            var appointmentLst = blavailable.GetAvailableTimingByVIP(VIPID);
            return Json(appointmentLst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(BOAvailableTiming model)
        {
            model.VipId = VIPID;
            int date = DateTime.Compare(DateTime.Now, Convert.ToDateTime(model.Date)); //now < myone ==>-1
            int results = DateTime.Compare(Convert.ToDateTime(model.EndTime), Convert.ToDateTime(model.StartTime));

            if (date <= 0 && results > 0)
            {

                int result = available.AddAvailableTiming(model);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(-1, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult Update(BOAvailableTiming model)
        {
            model.VipId = VIPID;
            int date = DateTime.Compare(DateTime.Now, Convert.ToDateTime(model.Date)); //now < myone ==>-1
            int results = DateTime.Compare(Convert.ToDateTime(model.EndTime), Convert.ToDateTime(model.StartTime));
            if (date <= 0 && results > 0)
            {
                var appointment = available.UpdateAvailableTiming(model);
                return Json(appointment, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetbyID(int Id)
        {
            var appoint = available.GetIndividualAvailableTiming(Id);
            return Json(appoint, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            var temp = available.DeleteAvailableTimings(ID);
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        BLLAppointmentDetails bllappointmentdetails = new BLLAppointmentDetails();
        [HttpGet]
        public JsonResult GetNotifications()
        {
            return Json(bllappointmentdetails.GetAllAppointment(), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult FixAppointment(BOAvailableTiming model)
        {
            if (ModelState.IsValid)
            {
                model.VipId = VIPID;
                available.AddAvailableTiming(model);

            }
            return View();

        }


        public ActionResult ViewAppointment()
        {
            string email = System.Web.HttpContext.Current.User.Identity.GetUserName();
            var temp = bllAppointment.getBookAppointmentByUser(email);


            return View(temp);
        }


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
        
        //yogesh part to add functionality to cancel appointment

        public ActionResult ViewAppointmentDone()
        {
            
            var model = bllAppointment.getAppointmentByVIP(VIPID);
            return View(model);
        }

        public ActionResult AppointmentDetails(int id)
        {
            var model = bllAppointment.getAppointmentByVIP(VIPID).Where(u => u.Id == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult AppointmentDetails(BOVipViewModel model)
        {
            BODateTime datetime = new BODateTime();
            datetime.AppointmentId = model.Id;
            datetime.Date = model.Date;
            datetime.Id = model.DateTimeId;
            datetime.FromTime = model.FromTime;
            datetime.ToTime = model.ToTime;
            datetime.IsCanceled = model.IsCanceled;

            bllDateTime.UpdateDateTime(datetime);
            return RedirectToAction("ViewAppointmentDone");
        }

    }

    



}
//€uv