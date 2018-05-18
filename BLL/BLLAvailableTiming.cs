using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;

namespace BLL
{
    public class BLLAvailableTiming
    {
        AppointmentDatabaseEntities _db = new AppointmentDatabaseEntities();
        public int AddAvailableTiming(BOAvailableTiming model)
        {
            using (_db)
            {
                tblAvailableTiming available = new tblAvailableTiming();
                available.Date = model.Date;
                available.StartTime = model.StartTime;
                available.EndTime = model.EndTime;
                available.IsAvailable = model.IsAvailable;
                available.VipId = model.VipId;
                _db.tblAvailableTimings.Add(available);
                return _db.SaveChanges();
            }
        }

        public int UpdateAvailableTiming(BOAvailableTiming model)
        {
            using (_db)
            {
                tblAvailableTiming available = _db.tblAvailableTimings.Where(u => u.Id == model.Id).FirstOrDefault();
                available.Date = model.Date;
                available.StartTime = model.StartTime;
                available.EndTime = model.EndTime;
                available.IsAvailable = model.IsAvailable;
                available.VipId = model.VipId;
                return _db.SaveChanges();
            }
        }

        public List<BOAvailableTiming> GetAllAvailableTiming()
        {
            using (_db)
            {
                List<BOAvailableTiming> lst = new List<BOAvailableTiming>();
                var temp = _db.tblAvailableTimings.ToList();
                foreach (var model in temp)
                {
                    BOAvailableTiming available = new BOAvailableTiming();
                    available.Id = model.Id;
                    available.Date = model.Date;
                    available.StartTime = model.StartTime;
                    available.EndTime = model.EndTime;
                    available.IsAvailable = model.IsAvailable;
                    available.VipId = model.VipId;
                    lst.Add(available);
                }
                return lst;
            }
        }

        public int DeleteAvailableTimings(int id)
        {
            using (_db)
            {
                tblAvailableTiming available = _db.tblAvailableTimings.Where(u => u.Id == id).FirstOrDefault();
                _db.tblAvailableTimings.Remove(available);
                return _db.SaveChanges();
            }
        }

        public List<BOAvailableTiming> GetAvailableTimingByVIP(int id)
        {
            using (_db)
            {
                List<BOAvailableTiming> lst = new List<BOAvailableTiming>();
                var temp = _db.tblAvailableTimings.Where(u => u.VipId == id).ToList();
                foreach (var model in temp)
                {
                    BOAvailableTiming available = new BOAvailableTiming();

                    available.Date = model.Date;
                    available.StartTime = model.StartTime;
                    available.Id = model.Id;
                    available.EndTime = model.EndTime;
                    available.IsAvailable = model.IsAvailable;
                    available.VipId = model.VipId;
                    lst.Add(available);
                }
                return lst;
            }
        }

        public BOAvailableTiming GetIndividualAvailableTiming(int id)
        {
            using ( _db )
            {
                var temp = _db.tblAvailableTimings.Where(u => u.Id == id).SingleOrDefault();
                BOAvailableTiming available = new BOAvailableTiming();
                available.Id = temp.Id;
                available.StartTime = temp.StartTime;
                available.Date = temp.Date;
                available.EndTime = temp.EndTime;
                available.IsAvailable = temp.IsAvailable;
                available.VipId = temp.VipId;
                return available;
            }
        }
    }
}
