using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;


namespace BLL
{
    public class BLLDateTime
    {
        AppointmentDatabaseEntities _db = new AppointmentDatabaseEntities();
        public int CreateDateTime(BODateTime model)
        {
            tblDateTime datetime = new tblDateTime();
            datetime.Date=model.Date;
            datetime.FromTime=model.FromTime;
            datetime.ToTime=model.ToTime;
            datetime.IsCanceled=model.IsCanceled;
            _db.tblDateTimes.Add(datetime);
            return _db.SaveChanges();
        }

        public int UpdateDateTime(BODateTime model)
        {
            tblDateTime datetime = _db.tblDateTimes.Where(u => u.Id == model.Id).FirstOrDefault();
            datetime.Id = model.Id;
            datetime.Date=model.Date;
            datetime.FromTime=model.FromTime;
            datetime.ToTime=model.ToTime;
            datetime.IsCanceled=model.IsCanceled;
            return _db.SaveChanges();
        }

        public List<BODateTime> GetAllDateTime()
        {
            List<BODateTime> lst = new List<BODateTime>();
            var temp = _db.tblDateTimes.ToList();
            foreach (var model in temp)
            {
                BODateTime datetime = new BODateTime();
                datetime.Date = model.Date;
                datetime.Id = model.Id;
                datetime.FromTime = model.FromTime;
                datetime.ToTime = model.ToTime;
                datetime.IsCanceled = model.IsCanceled;
                lst.Add(datetime);
            }
            return lst;
        }

        public int DeleteDateTime(int id)
        {
            tblDateTime datetime = _db.tblDateTimes.Where(u => u.Id == id).FirstOrDefault();
            _db.tblDateTimes.Remove(datetime);
            return _db.SaveChanges();
        }

        public int GetLastId()
        {
           return _db.tblDateTimes.Max(u => u.Id);
        }

       
    }
}
