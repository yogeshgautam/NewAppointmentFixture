using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BLLUser
    {
        AppointmentDatabaseEntities _db = new AppointmentDatabaseEntities();
        public List<BOUsers> GetAllUsers()
        {
            List<BOUsers> lst = new List<BOUsers>();
            var temp = _db.AspNetUsers.ToList();
            foreach (var item in temp)
            {
                BOUsers users = new BOUsers();
                users.Email = item.Email;
                users.UserName = item.UserName;
                users.Id = item.Id;
                lst.Add(users);
            }
            return lst;
        }

  
    }
}
