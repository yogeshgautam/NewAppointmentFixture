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
        BLLCompany    bllcompany;
        BLLDepartment blldepartment;
        BLLVIP bllVIP;
        BLLAvailableTiming bllAvailable;
        BLLDateTime bllDateTime;
        BLLUser bllUser;
        BLLAppointmentDetails bllAppointment;
        public static string emailId;
        public static string VipEmail;
        public static string selectedDate;
     
       public static List<BOAppointmentDetails> barray = new List<BOAppointmentDetails>();

        public UserController( )
        {
            bllcompany = new BLLCompany();
            blldepartment = new BLLDepartment();
            bllVIP = new BLLVIP();
            bllAvailable = new BLLAvailableTiming();
            bllDateTime = new BLLDateTime();
            bllUser = new BLLUser();
            bllAppointment = new BLLAppointmentDetails();
            string id = System.Web.HttpContext.Current.User.Identity.GetUserName();
            emailId = id; // emailId of User

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


        public JsonResult loadDepartment(int Id)
        {
            ViewBag.Company = bllcompany.GetAllCompany();
            var a = blldepartment.getDepartmentByCountryId(Id);
            return Json(a, JsonRequestBehavior.AllowGet);
        }


        public JsonResult loadVIP(int Id)  //Id of Department
        {
            var temp = bllVIP.GetAllVIPByDepartmentId(Id);
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult loadDate(int Id)  //Id of VIP
        {
            var emails = bllVIP.GetAllVIP().Where(u => u.Id == Id).Select(x => new { x.Email }).SingleOrDefault();
            VipEmail = emails.Email;
            var appointmentOnThatDate = bllAppointment.GetAllAppointment().Where(x => x.AppointmentTo == emails.Email).ToList();

            foreach (var item in appointmentOnThatDate)
            {
                BOAppointmentDetails ba = new BOAppointmentDetails();
                ba.DateTimeId = item.DateTimeId;
                barray.Add(ba);
            }
            var temp = bllAvailable.GetAllAvailableTiming().Where(u => u.VipId == Id && u.IsAvailable == true).Select(u => u.Date).Distinct().ToList();
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult loadTime(string Id)
        {
            selectedDate = Id;
            var temp = bllAvailable.GetAllAvailableTiming().Where(u => u.Date==Id && u.IsAvailable==true).ToList();
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult loadFixedTime(int Id)
        {
            var temp = bllAvailable.GetAllAvailableTiming().Where(u => u.Id == Id).SingleOrDefault();
            var a = temp.StartTime;
            var b = temp.EndTime;

            ///
            DateTime StartDate = DateTime.Parse(a);
            DateTime EndDate = DateTime.Parse(b);
            int MinInterval = 15;

           


            List<string> dateList = new List<string>();
            while (StartDate <= EndDate)
            {
                dateList.Add(StartDate.ToString("HH:mm"));
                StartDate = StartDate.AddMinutes(MinInterval);
            }

            var lstofDate = bllAppointment.getBookAppointmentByUser(VipEmail).Where(u => u.Date == Convert.ToDateTime(selectedDate) && u.IsCanceled==true).ToList();
            List<string> dateList1 = new List<string>();
                foreach (var items in lstofDate)
                {  
                    dateList1.Add(items.FromTime.ToString());
                    //dateList1.Add(items.ToTime.ToString());
                }
            var listdistinct = dateList.Except(dateList1);
            return Json(listdistinct, JsonRequestBehavior.AllowGet);
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
            //var dateValue = bllAvailable.GetAllAvailableTiming().Where(u => u.Id == Convert.ToInt32(dateId)).SingleOrDefault().Date; ////date

            bDateTime.Date = Convert.ToDateTime(dateId);
            //string resultString = Regex.Match(bDateTime.Date.ToString(), @"\d{4}-\d{2}-\d{2}").Value;
            bDateTime.FromTime = fc["dropdownInterval"].ToString().Trim();
            bDateTime.ToTime = endTime.Trim();
            bDateTime.IsCanceled = false;
            int i = bllDateTime.CreateDateTime(bDateTime);

            bAppointment.DepartmentId = Convert.ToInt32(fc["dropdownDepartment"].ToString());
            bAppointment.AppointmentFrom = emailId.Trim();
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

        public ActionResult DetailsUserAppointment(int id)
        {


            var model=bllAppointment.getAppointmentDetailsByUser(emailId);
            return View(model.SingleOrDefault(u=>u.Id==id));
        }

        public ActionResult ViewAppointmentList()
        {
            List<BOVipViewModel> boVipViewModel = bllAppointment.getAppointmentDetailsByUser(emailId);
            return Json(boVipViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewAppointmentById(int id)
        {
            List<BOVipViewModel> boVipViewModel = bllAppointment.getAppointmentDetailsByUser(emailId);
            var temp = boVipViewModel.Where(u => u.Id == id).FirstOrDefault();
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPaggedData(int pageNumber = 1, int pageSize = 5)
        {
            List<BOVipViewModel> boVipViewModel = bllAppointment.getAppointmentDetailsByUser(emailId);
            var pagedData = Pagination.PagedResult(boVipViewModel, pageNumber, pageSize);
            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }
    }
}