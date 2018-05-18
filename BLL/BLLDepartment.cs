using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;

namespace BLL
{
    public class BLLDepartment
    {
        AppointmentDatabaseEntities _db = new AppointmentDatabaseEntities();
        BLLCompany blcompany = new BLLCompany();
        public int CreateDepartment(BODepartment model)
        {
            tblDepartment department = new tblDepartment();
            department.CompanyId = model.CompanyId;
            department.phone = model.phone;
            department.Email = model.Email;
            department.Details = model.Details;
            department.IsDeleted = false;
            department.Name = model.Name;
            department.CreatedAt = DateTime.Now.ToString();
            department.HOD = model.HOD;
            _db.tblDepartments.Add(department);
            return _db.SaveChanges();
        }

        public int UpdateDepartment(BODepartment model)
        {
            tblDepartment department = _db.tblDepartments.Where(u => u.Id == model.Id).FirstOrDefault();
            department.phone = model.phone;
            department.Email = model.Email;
            department.Details = model.Details;
            department.Name = model.Name;
            department.LastUpdated = DateTime.Now.ToString();
            department.HOD = model.HOD;
            return _db.SaveChanges();
        }

        public List<BODepartment> GetAllDepartment()
        {
            List<BODepartment> lst = new List<BODepartment>();
            var temp = _db.tblDepartments.Where(u=>u.IsDeleted==false).ToList();
            foreach (var model in temp)
            {
                BODepartment department = new BODepartment();
                department.CompanyId = model.CompanyId;
                department.lstCompany = blcompany.GetCompanyById(model.CompanyId.Value);
                department.phone = model.phone;
                department.Email = model.Email;
                department.Details = model.Details;
                department.Id = model.Id;
                department.Name = model.Name;
                department.HOD = model.HOD;
                lst.Add(department);
            }
            return lst;
        }

        public BODepartment GetDepartmentById(int id)
        {
            tblDepartment model = _db.tblDepartments.Where(u => u.Id == id).FirstOrDefault();
            BODepartment department = new BODepartment();
            department.CompanyId = model.CompanyId;
            department.lstCompany = blcompany.GetCompanyById(model.CompanyId.Value);
            department.phone = model.phone;
            department.Details = model.Details;
            department.Email = model.Email;
            department.Id = model.Id;
            department.Name = model.Name;
            department.HOD = model.HOD;
            return department;
        }

        public int DeleteDepartment(int id)
        {
            tblDepartment department = _db.tblDepartments.Where(u => u.Id == id).FirstOrDefault();
            department.IsDeleted = true;
            //_db.tblDepartments.Remove(department);
            return _db.SaveChanges();
        }

        public object getDepartmentByCountryId(int id)
        {

            List<BODepartment> lst = new List<BODepartment>();
            var temp = _db.tblDepartments.Where(u => u.CompanyId == id).ToList();
            foreach (var item in temp)
            {
                BODepartment bdepartment = new BODepartment();
                bdepartment.Id = item.Id;
                bdepartment.CompanyId = item.CompanyId;
                bdepartment.Name = item.Name;
                lst.Add(bdepartment);
            }
            return lst;
        }


    }
}
