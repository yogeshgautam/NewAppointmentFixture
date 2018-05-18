using BLL;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace AppointmentFixturesProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        BLLCompany bllcompany;
        BLLDepartment blldepartment;
        BLLVIP bllVIP;
        BLLAvailableTiming bllAvailable;
        BLLDateTime bllDateTime;
        BLLUser bllUser;
        BLLAppointmentDetails bllAppointment;
        public static string emailId;

        public UserController()
        {
            bllcompany = new BLLCompany();
            blldepartment = new BLLDepartment();
            bllVIP = new BLLVIP();
            bllAvailable = new BLLAvailableTiming();
            bllDateTime = new BLLDateTime();
            bllUser = new BLLUser();
            bllAppointment = new BLLAppointmentDetails();
            string id = System.Web.HttpContext.Current.User.Identity.GetUserName();
            emailId = id;
        }

        public static string endTime;

        // GET: User
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult FixAppointment()
        {
            var a = bllcompany.GetAllCompany();
            ViewBag.Company = a;
            return View();
        }


        public ActionResult loadDepartment(int Id)
        {
            ViewBag.Company = bllcompany.GetAllCompany();
            var a = blldepartment.getDepartmentByCountryId(Id);
            return Json(a, JsonRequestBehavior.AllowGet);
        }


        public ActionResult loadVIP(int Id)  //Id of Department
        {
            var temp = bllVIP.GetAllVIPByDepartmentId(Id);
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadDate(int Id)  //Id of VIP
        {
            var temp = bllAvailable.GetAllAvailableTiming().Where(u => u.VipId == Id).ToList();

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadTime(int Id)
        {
            var temp = bllAvailable.GetAllAvailableTiming().Where(u => u.Id == Id).ToList();
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadFixedTime(int Id)
        {
            var temp = bllAvailable.GetAllAvailableTiming().Where(u => u.Id == Id).SingleOrDefault();
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveEndInterval(string Id)
        {
            endTime = Id.Substring(Id.Length - 8);
            return View();
        }

        [HttpPost]
        public ActionResult SaveToDateTimeTable(FormCollection fc)
        {
            BODateTime bDateTime = new BODateTime();
            BOAppointmentDetails bAppointment = new BOAppointmentDetails();
            var companyId = fc["dropdownCompany"].ToString(); //CompanyId
            var vipId = fc["dropdownVIP"].ToString();   //VipId
            var dateId = fc["dropdownDate"].ToString();
            var dateValue = bllAvailable.GetAllAvailableTiming().Where(u => u.Id == Convert.ToInt32(dateId)).SingleOrDefault().Date; ////date

            bDateTime.Date = Convert.ToDateTime(dateValue);
            //string resultString = Regex.Match(bDateTime.Date.ToString(), @"\d{4}-\d{2}-\d{2}").Value;
            bDateTime.FromTime = fc["dropdownInterval"].ToString();
            bDateTime.ToTime = endTime;
            bDateTime.IsCanceled = false;
            int i = bllDateTime.CreateDateTime(bDateTime);

            bAppointment.DepartmentId = Convert.ToInt32(fc["dropdownDepartment"].ToString());
            bAppointment.AppointmentFrom = emailId;
            bAppointment.AppointmentTo = bllVIP.GetAllVIP().Where(u => u.Id == Convert.ToInt32(vipId)).SingleOrDefault().Email;
            bAppointment.DateTimeId = bllDateTime.GetLastId();
            bAppointment.Details = fc["Details"].ToString();
            bAppointment.status = "True";
            int j = bllAppointment.CreateAppointment(bAppointment);
            if (j > 0)
            {
                ViewBag.Appointment = "Successfully Created";
            }
            else
            {
                ViewBag.Appointment = "Failed";
            }
            return RedirectToAction("FixAppointment");

        }

        public ActionResult ViewSetAppointmentDetails()
        {
            List<BOVipViewModel> boVipViewModel = bllAppointment.getAppointmentDetailsByUser(emailId);
            return View(boVipViewModel);
        }

        //public ActionResult EditUserAppointment()
        //{

        //}


        public ActionResult DeleteUserAppointment(int id)
        {
            //var boVipViewModal = bllAppointment.GetAllAppointment().Where(u=>user)
            //return View(boVipViewModal);
            return View();
        }

        [HttpPost]
        public ActionResult DeleteUserAppointment(BOVipViewModel model)
        {
            int i = bllDateTime.DeleteDateTime(model.Id);
            if (i > 0)
            {
                int j = bllAppointment.DeleteAppointmentOnTheBasisOfDeletedDateTime(model.Id);
                if (j > 0)
                {
                    ViewBag.AppointmentDetails = "Successfully Deleted user data";
                }
            }
            return View();
        }
    }
}