using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BO;
using System.Globalization;

namespace AppointmentFixturesProject.Controllers
{
    [Authorize]
    public class ScheduleVIPController : Controller
    {
        public static int VIPID;

        BLLAvailableTiming blavailable = new BLLAvailableTiming();
        BLLVIP blvip = new BLLVIP();
        BLLCompany bllCompany = new BLLCompany();
        BLLAppointmentDetails bllAppointment = new BLLAppointmentDetails();
        BLLDateTime bllDateTime = new BLLDateTime();
        // GET: ScheduleVIP

        public ScheduleVIPController()
        {

        }


        public ActionResult VIP(int id)
        {
            VIPID = id;
            //List<BOAvailableTiming> lst = blavailable.GetAvailableTimingByVIP(id).ToList();
            return RedirectToAction("FixAppointment");
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

        public ActionResult GetPaggedData(int pageNumber = 1, int pageSize = 5)
        {
           var listData = blavailable.GetAvailableTimingByVIP(VIPID).OrderByDescending(u=>u.Id).ToList();
            var pagedData = Pagination.PagedResult(listData, pageNumber, pageSize);
            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(BOAvailableTiming model)
        {
            model.VipId = VIPID;
            int hasData = checkIntoDatabase(model);
            if (hasData == 0)
            {
                int date = compareDate(model.Date);
                int time = compareTime(model.EndTime, model.StartTime);
                if (date <= 0 && time > 0)
                {
                    int result = blavailable.AddAvailableTiming(model);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(-1, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                TempData["message"] = "some message that you want to display";
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult Update(BOAvailableTiming model)
        {
            model.VipId = VIPID;

            int date = compareDate(model.Date);
            int time = compareTime(model.EndTime, model.StartTime);
            if (date <= 0 && time > 0)
            {
                var appointment = blavailable.UpdateAvailableTiming(model);
                return Json(appointment, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
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


        public ActionResult CreateTiming()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTiming(BOAvailableTiming model)
        {
            model.VipId = VIPID;
            blavailable.AddAvailableTiming(model);
            return RedirectToAction("VIP", new { id = VIPID });
        }

        public ActionResult EditAvailableTiming(int id)
        {
            BOAvailableTiming model = blavailable.GetIndividualAvailableTiming(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditAvailableTiming(BOAvailableTiming model)
        {
            blavailable.UpdateAvailableTiming(model);
            return RedirectToAction("VIP", new { id = VIPID });
        }

        public ActionResult DeleteAvailableTiming(int id)
        {
            blavailable.DeleteAvailableTimings(id);
            return RedirectToAction("VIP", new { id = VIPID });
        }

        public int compareDate(string date)
        {
            int bdate = DateTime.Compare(DateTime.Now, Convert.ToDateTime(date)); //now < myone ==>-1
            if (DateTime.Now.Date == Convert.ToDateTime(date))
            {
                bdate = 0;
            }
            return bdate;
        }

        public int compareTime(string endTime, string startTime)
        {
            //DateTime utcTime = DateTime.UtcNow;
            //TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Nepal Standard Time");
            //DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
            int results;
            //int results = DateTime.Compare(Convert.ToDateTime(startTime), Convert.ToDateTime(localTime));
            //if (results >= 0)
            //{
                results = DateTime.Compare(Convert.ToDateTime(endTime), Convert.ToDateTime(startTime));
            //}

            return results;
        }

        public int checkIntoDatabase(BOAvailableTiming model)
        {
            int i = 0;
            DateTime startTime = Convert.ToDateTime(model.StartTime);
            DateTime endTime = Convert.ToDateTime(model.EndTime);
            var templst = blavailable.GetAllAvailableTiming().Where(u => u.VipId == VIPID && u.Date == model.Date).ToList();
            foreach (var item in templst)
            {
                DateTime start = Convert.ToDateTime(item.StartTime);
                DateTime end = Convert.ToDateTime(item.EndTime);
                if (((start <= startTime) && (end >= startTime)) || (start <= endTime && end >= endTime))
                {
                    i = 1;
                    break;
                }
            }
            return i;
        }



    }
}