using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;
using BLL;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.Net;
using PagedList;
using PagedList.Mvc;
using Jquery_Mvc_Paging.Helper;
namespace AppointmentFixturesProject.Controllers
{

     [Authorize(Roles = "CompanyMaster")]
    public class CompanyController : Controller
    {
        //BLL
        BLLDepartment bllDepartment = new BLLDepartment();
        BLLCompany bllCompany=new BLLCompany();
        BLLVIP bllvip = new BLLVIP();
        BLLUser bluser = new BLLUser();
        BLLAvailableTiming blavailable = new BLLAvailableTiming();
        BLLMeetingFirst bllMeetingFirst = new BLLMeetingFirst();
        BLLAppointmentDetails bllappointmentdetails = new BLLAppointmentDetails();
        //BLLEnds


         public static int companyId=1;
         public static string companyName = "";
         public CompanyController()
         {

             string id = System.Web.HttpContext.Current.User.Identity.GetUserId();
             var lst = bllCompany.GetAllCompany().Where(u => u.UserId == id).FirstOrDefault();
             companyId = lst.Id;
             companyName = lst.Name;
             ViewBag.Companyname = lst.Name;
         }
         //this method is called to get the application name in the navigation bar
         public string CompanyName()
         {
             string id = User.Identity.GetUserId();
             var lst = bllCompany.GetAllCompany().Where(u => u.UserId == id).FirstOrDefault();
             companyId = lst.Id;
             companyName = lst.Name;
             return lst.Name;
         }
         public ActionResult partialComanyName()
         {
             return PartialView("_CompanyNamePartial",companyName);
         }
        //
        //Department Functions
        public ActionResult Index(string search, int? page)
        {

            return View();
        }
        public ActionResult GetPaggedData(int pageNumber = 1, int pageSize = 5)
        {
            var listData = bllDepartment.GetAllDepartment().OrderByDescending(u => u.Id).ToList();
            var pagedData = Pagination.PagedResult(listData, pageNumber, pageSize);
            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddDepartments(BODepartment department)
        {
            department.CompanyId = companyId;
            var ae = bllDepartment.CreateDepartment(department);
            return Json(ae, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyIDDepartments(int id)
        {
            var ae = bllDepartment.GetDepartmentById(id);
            return Json(ae, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateDepartments(BODepartment department)
        {
            var ae = bllDepartment.UpdateDepartment(department);
            return Json(ae, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteDepartments(int ID)
        {
            var ae = bllDepartment.DeleteDepartment(ID);
            return Json(ae, JsonRequestBehavior.AllowGet);
        }
        //Department Function Ends

        //VIPFunction
        public ActionResult VIP(string search,int?page)
        {
            List<BOVIPTable> lst = bllvip.GetAllVIP().Where(u=>u.lstDepartment.CompanyId==companyId).ToList();
            if (search != null)
            {
                lst = bllvip.GetAllVIP().Where(u => u.lstDepartment.CompanyId == companyId&&u.FullName.StartsWith(search)).ToList();
            }
            return View(lst.ToPagedList(page??1,10));
        }
        public ActionResult CreateVIP()
        {
            var x = bllDepartment.GetAllDepartment().Where(u=>u.CompanyId==companyId).ToList();
            ViewBag.Department = x;

            var y = bluser.GetAllUsers();
            ViewBag.Users = y;

            return View();
        }
        [HttpPost]
        public ActionResult CreateVIP(BOVIPTable model)
        {
           
            if (bllvip.CreateVIP(model) == 1)
            {
                Roles.RemoveUserFromRole(model.Email, "CompanyMaster");
                Roles.AddUserToRole(model.Email, "COMPANYVIP");
                //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                //RedirectToAction("CreateVIP", "MyAccountController", new { model = model });
                //AccountController ac = new AccountController();
                //string userid=User.Identity.GetUserId();   
                //var roleresult =ac.UserManager.AddToRole(model.UserId, "CompanyMaster");
                ViewBag.Message = "VIP Created Success ! ";
                //Roles.AddUsersToRole(User.Identity., "CompanyMaster");
                
            }
            else
            {
                ViewBag.Message = "There was a problem creating VIP. Please contact Administrator for details. ";
            }

            ViewBag.Department = bllDepartment.GetAllDepartment().Where(u => u.CompanyId == companyId).ToList();
            var y = bluser.GetAllUsers();
            ViewBag.Users = y;
            return View();
        }
        public ActionResult DeleteVIP(int id)
        {
            BOVIPTable model = bllvip.GetVIPById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteVIP(BOVIPTable model)
        {
            bllvip.DeleteVIP(model.Id);
            return RedirectToAction("VIP");
        }
        public ActionResult EditVIP(int id)
        {
            BOVIPTable vip = bllvip.GetVIPById(id);
            ViewBag.Department = bllDepartment.GetAllDepartment();
            return View(vip);
        }
        [HttpPost]
        public ActionResult EditVIP(BOVIPTable model)
        {
            bllvip.UpdateVIP(model);
            ViewBag.Department = bllDepartment.GetAllDepartment();
            return RedirectToAction("VIP");
        }
        public ActionResult DetailsVIP(int id)
        {
            BOVIPTable vip= bllvip.GetVIPById(id);
            return View(vip);
        }
        public ActionResult ScheduleVip(int id)
         {
             List<BOAvailableTiming> lst = blavailable.GetAvailableTimingByVIP(id);
             return View(lst);
             
         }
        //VIP Function Ends

       //ViewMeetingOne 
        [HttpGet]
         public ActionResult CreateMeetingOne()
         {
             BOMeetingFirst meeting = new BOMeetingFirst();
             var temp = bllvip.GetAllVIP().Where(u => u.lstDepartment.CompanyId == companyId).ToList();
             ViewBag.Company = temp;
             return View(meeting);
         }
         [HttpPost]
         public ActionResult CreateMeetingOne(BOMeetingFirst model)
         {
             ViewBag.Company = bllvip.GetAllVIP().Where(u => u.lstDepartment.CompanyId == companyId).ToList();
             if (ModelState.IsValid)
             {
                 var i = bllMeetingFirst.AddMeetingFirst(model);
                 if (i > 0)
                 {
                     ViewBag.Message = "Meeting has been created";
                 }
                 return RedirectToAction("ViewMeetingOne");
             }
             return View();
         }
         [HttpGet]
         public ActionResult ViewMeetingOne()
         {
             //var ae = bllMeetingFirst.GetALLMeeting();
             //foreach (var item in ae)
             //{
             //    var a = bllvip.GetVIPById(Convert.ToInt32(item.VIPuser));
             //    item.VipName = a.FullName;
             //}

             return View();
         }
         public JsonResult List()
         {
             var ae = bllMeetingFirst.GetALLMeeting();
             return Json(ae, JsonRequestBehavior.AllowGet);
         }
         public JsonResult UpdateMeetingOne(BOMeetingFirst model)
         {
             var ae = bllMeetingFirst.UpdateMeetingOne(model);
             return Json(ae, JsonRequestBehavior.AllowGet);
         }
         public JsonResult Delete(int ID)
         {
             var ae = bllMeetingFirst.DeleteMeeting(ID);
             return Json(ae, JsonRequestBehavior.AllowGet);
         }
         public JsonResult GetById(int id)
         {
             var ae = bllMeetingFirst.GetALLMeetingByID(id);
             return Json(ae, JsonRequestBehavior.AllowGet);

         }
         public JsonResult Add(BOMeetingFirst model)
         {
             var ae = bllMeetingFirst.AddMeetingFirst(model);
             return Json(ae, JsonRequestBehavior.AllowGet);
         }
        //ViewMeetingOne Ends

        //NotificationFunction
        [HttpGet]
        public JsonResult GetNotifications()
        {
            return Json(bllappointmentdetails.GetAllAppointment(), JsonRequestBehavior.AllowGet);
        }
        //NotificationFunctionends
    }
}