using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;


namespace BLL
{
    public class BLLVIP
    {
        AppointmentDatabaseEntities _db = new AppointmentDatabaseEntities();
        BLLDepartment bldepartment = new BLLDepartment();
        public int CreateVIP(BOVIPTable model)
        {
            tblVIPUser vip = new tblVIPUser();
            vip.Address1 = model.Address1;
            vip.Address2 = model.Address2;
            vip.CreatedAt = DateTime.Now.ToString();
            vip.DepartmentId = model.DepartmentId;
            vip.UserId = model.UserId;
            vip.Designation = model.Designation;
            vip.Details = model.Details;
            vip.Email = model.Email;
            vip.FullName = model.FullName;
            vip.IsDeleted = false;
            vip.Phone = model.Phone;
            vip.Photo = model.Photo;
            _db.tblVIPUsers.Add(vip);
            return _db.SaveChanges();            
        }

        public int UpdateVIP(BOVIPTable model)
        {
            tblVIPUser vip = _db.tblVIPUsers.Where(u => u.Id == model.Id).FirstOrDefault();
            vip.Address1 = model.Address1;
            vip.Address2 = model.Address2;
            vip.LastUpdated = DateTime.Now.ToString();
            //vip.DepartmentId = model.DepartmentId;
            vip.Designation = model.Designation;
            vip.Details = model.Details;
            vip.Email = model.Email;
            vip.FullName = model.FullName;
            vip.Phone = model.Phone;
            vip.Photo = model.Photo;
          //  vip.UserId = model.UserId;
            return _db.SaveChanges();
        }

        public List<BOVIPTable> GetAllVIP()
        {
            List<BOVIPTable> lst = new List<BOVIPTable>();
            var temp = _db.tblVIPUsers.Where(u=>u.IsDeleted==false).ToList();
            foreach (var model in temp)
            {
                BOVIPTable vip = new BOVIPTable();
                vip.Id = model.Id;
                vip.Address1 = model.Address1;
                vip.Address2 = model.Address2;
                vip.LastUpdated = DateTime.Now.ToString();
                vip.DepartmentId = model.DepartmentId;
                vip.lstDepartment = bldepartment.GetDepartmentById(model.DepartmentId.Value);
                vip.Designation = model.Designation;
                vip.Details = model.Details;
                vip.Email = model.Email;
                vip.FullName = model.FullName;
                vip.Phone = model.Phone;
                vip.Photo = model.Photo;
                vip.UserId = model.UserId;
                lst.Add(vip);
            }
            return lst;
        }

        public BOVIPTable GetVIPById(int id)
        {
            tblVIPUser model = _db.tblVIPUsers.Where(u => u.Id == id&&u.IsDeleted==false).FirstOrDefault();
            BOVIPTable vip = new BOVIPTable();
            vip.Id = model.Id;
            vip.Address1 = model.Address1;
            vip.Address2 = model.Address2;
            vip.DepartmentId = model.DepartmentId;
            vip.lstDepartment = bldepartment.GetDepartmentById(model.DepartmentId.Value);
            vip.Designation = model.Designation;
            vip.Details = model.Details;
            vip.Email = model.Email;
            vip.FullName = model.FullName;
            vip.Phone = model.Phone;
            vip.Photo = model.Photo;
            vip.UserId = model.UserId;
            return vip;
        }

        public int DeleteVIP(int id)
        {
            tblVIPUser vip = _db.tblVIPUsers.Where(u => u.Id == id).FirstOrDefault();
            vip.IsDeleted = true;
            //_db.tblVIPUsers.Remove(vip);
            return _db.SaveChanges();
        }

        public List<BOVIPTable> GetAllVIPByDepartmentId(int id)
        {
            List<BOVIPTable> lst = new List<BOVIPTable>();
            var temp = _db.tblVIPUsers.Where(u => u.IsDeleted == false && u.DepartmentId == id).ToList();
            foreach (var model in temp)
            {
                BOVIPTable vip = new BOVIPTable();
                vip.Id = model.Id;
                vip.UserId = model.UserId;
                vip.FullName = model.FullName;
                lst.Add(vip);
            }
            return lst;
        }

    }
}
