using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BO;

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
            List<BOAvailableTiming> lst = blavailable.GetAvailableTimingByVIP(id).ToList();
            return View(lst);
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

            return RedirectToAction("VIP", new { id =VIPID});
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



    }
}