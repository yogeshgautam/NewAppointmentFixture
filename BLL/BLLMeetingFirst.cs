
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BO;
using DAL;

namespace BLL
{
   public class BLLMeetingFirst
    {
        AppointmentDatabaseEntities _db = new AppointmentDatabaseEntities();
        BLLVIP blvip = new BLLVIP();
        public int AddMeetingFirst(BOMeetingFirst model)
        {
            tblMeetingFirst meeting = new tblMeetingFirst();
            meeting.VIPUser = model.VIPuser;
            meeting.Users = model.Users;
            meeting.StartTime = model.StartTime;
            meeting.EndTime = model.EndTime;
            meeting.Date = Convert.ToDateTime(model.Date);
            _db.tblMeetingFirsts.Add(meeting);
            return _db.SaveChanges();
        }
        public int UpdateMeetingOne(BOMeetingFirst model)
        {
            tblMeetingFirst meeting = _db.tblMeetingFirsts.Where(u => u.ID == model.ID).FirstOrDefault();
            meeting.VIPUser = model.VIPuser;
            meeting.Users = model.Users;
            meeting.StartTime = model.StartTime;
            meeting.EndTime = model.EndTime;
            meeting.Date = Convert.ToDateTime(model.Date);

            return _db.SaveChanges();
        }
        public List<BOMeetingFirst> GetALLMeeting()
        {
            List<BOMeetingFirst> lst = new List<BOMeetingFirst>();
            var ls = _db.tblMeetingFirsts.ToList();
            
            foreach (var item in ls)
            {
                lst.Add(new BOMeetingFirst() { ID = item.ID, VIPuser = item.VIPUser, Users = item.Users, StartTime = item.StartTime, EndTime = item.EndTime, Date = item.Date.ToString() });
                    

            }
            
            return lst;
        }
        public BOMeetingFirst GetALLMeetingByID(int id)
        {
            var ls = _db.tblMeetingFirsts.Where(u => u.ID == id).FirstOrDefault();
            BOMeetingFirst ave = new BOMeetingFirst() { ID = ls.ID, VIPuser = ls.VIPUser, Users = ls.Users, StartTime = ls.StartTime, EndTime = ls.EndTime, Date = ls.Date.ToString() };
            return ave;

        }


        public int DeleteMeeting (int id)
        {
            tblMeetingFirst tb = _db.tblMeetingFirsts.Where(u => u.ID == id).FirstOrDefault();
            _db.tblMeetingFirsts.Remove(tb);
            return _db.SaveChanges();
        }
    }
}

