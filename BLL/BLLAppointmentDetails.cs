using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BLL
{
    public class BLLAppointmentDetails
    {
        AppointmentDatabaseEntities _db = new AppointmentDatabaseEntities();
        public int CreateAppointment(BOAppointmentDetails model)
        {
            tblAppointment appointment = new tblAppointment();
            appointment.DepartmentId = model.DepartmentId;
            appointment.AppointmentFrom = model.AppointmentFrom;
            appointment.AppointmentTo = model.AppointmentTo;
            appointment.DateTimeId = model.DateTimeId;
            appointment.Details = model.Details;
            _db.tblAppointments.Add(appointment);
            return _db.SaveChanges();
        }

        public int UpdateAppointment(BOAppointmentDetails model)
        {
            tblAppointment appointment = _db.tblAppointments.Where(u => u.Id == model.Id).FirstOrDefault();
            appointment.AppointmentFrom = model.AppointmentFrom;
            appointment.AppointmentTo = model.AppointmentTo;
            appointment.DateTimeId = model.DateTimeId;
            appointment.DepartmentId = model.DepartmentId;
            appointment.Details = model.Details;
            return _db.SaveChanges();
        }

        public List<BOAppointmentDetails> GetAllAppointment()
        {
            List<BOAppointmentDetails> lst = new List<BOAppointmentDetails>();
            var temp = _db.tblAppointments.ToList();
            foreach (var model in temp)
            {
                BOAppointmentDetails appointment = new BOAppointmentDetails();
                appointment.AppointmentFrom = model.AppointmentFrom;
                appointment.AppointmentTo = model.AppointmentTo;
                appointment.DateTimeId = model.DateTimeId;
                appointment.DepartmentId = model.DepartmentId;
                appointment.Details = model.Details;
                lst.Add(appointment);
            }
            return lst;
        }

        public int DeleteAppointment(int id)
        {
            tblAppointment appointment = _db.tblAppointments.Where(u => u.Id == id).FirstOrDefault();
            _db.tblAppointments.Remove(appointment);
            return _db.SaveChanges();
        }

        public List<BOVipViewModel> getBookAppointmentByUser(string email)
        {
            List<BOVipViewModel> lst = new List<BOVipViewModel>();
            var result = from p in _db.tblDateTimes
                         join q in _db.tblAppointments
                         on p.Id equals q.DateTimeId
                         where q.AppointmentTo == email
                         select new { p, q };

            foreach (var item in result)
            {
                BOVipViewModel bb = new BOVipViewModel();

                bb.Date = item.p.Date;
                ////string resultString = Regex.Match(item.p.Date.ToString(), @"\d{2}-\d{2}-\d{4}").Value;
                ////bb.Date = Convert.toresultString;
                //DateTime dt = DateTime.ParseExact(bb.Date.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                //string s = dt.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                bb.DepartmentId = item.q.DepartmentId;
                bb.FromTime = item.p.FromTime;
                bb.ToTime = item.p.ToTime;
                bb.AppointmentFrom = item.q.AppointmentFrom;
                bb.AppointmentTo = item.q.AppointmentTo;
                bb.Details = item.q.Details;
                bb.Id = item.p.Id;
                bb.IsCanceled = item.p.IsCanceled;
                lst.Add(bb);
            }
            return lst;
        }

        public List<BOVipViewModel> getAppointmentDetailsByUser(string emailId)
        {
            
            List<BOVipViewModel> lst = new List<BOVipViewModel>();
            var result = from p in _db.tblDateTimes
                         join q in _db.tblAppointments
                         on p.Id equals q.DateTimeId
                         where q.AppointmentFrom == emailId
                         select new { p, q };

            foreach (var item in result)
            {
                BOVipViewModel bb = new BOVipViewModel();

                bb.Date = item.p.Date;
                ////string resultString = Regex.Match(item.p.Date.ToString(), @"\d{2}-\d{2}-\d{4}").Value;
                ////bb.Date = Convert.toresultString;
                //DateTime dt = DateTime.ParseExact(bb.Date.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                //string s = dt.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                bb.DepartmentId = item.q.DepartmentId;
                bb.FromTime = item.p.FromTime;
                bb.ToTime = item.p.ToTime;
                bb.AppointmentFrom = item.q.AppointmentFrom;
                bb.AppointmentTo = item.q.AppointmentTo;
                bb.Details = item.q.Details;
                bb.Id = item.p.Id;
                bb.IsCanceled = item.p.IsCanceled;
                lst.Add(bb);
            }
            return lst;
        }

        public int DeleteAppointmentOnTheBasisOfDeletedDateTime(int id)
        {
            tblAppointment appointment = _db.tblAppointments.Where(u => u.DateTimeId == id).FirstOrDefault();
            _db.tblAppointments.Remove(appointment);
            return _db.SaveChanges();
        }

        public List<BOVipViewModel> GetAllStaffAppointmentDetails()
        {
            List<BOVipViewModel> lst = new List<BOVipViewModel>();
            var result = from p in _db.tblDateTimes
                         join q in _db.tblAppointments
                         on p.Id equals q.DateTimeId
                         select new { p, q };

            foreach (var item in result)
            {
                BOVipViewModel bb = new BOVipViewModel();

                bb.Date = item.p.Date;
                ////string resultString = Regex.Match(item.p.Date.ToString(), @"\d{2}-\d{2}-\d{4}").Value;
                ////bb.Date = Convert.toresultString;
                //DateTime dt = DateTime.ParseExact(bb.Date.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                //string s = dt.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                //bb.DepartmentId = item.q.DepartmentId;
                bb.FromTime = item.p.FromTime;
                bb.ToTime = item.p.ToTime;
                bb.AppointmentFrom = item.q.AppointmentFrom;
                bb.AppointmentTo = item.q.AppointmentTo;
                bb.Details = item.q.Details;
                bb.Id = item.p.Id;
                bb.IsCanceled = item.p.IsCanceled;
                lst.Add(bb);
            }
            return lst;
        }
    }
}
