using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;

namespace BLL
{
    public class BLLCompany
    {
        AppointmentDatabaseEntities _db = new AppointmentDatabaseEntities();
        public int CreateCompany(BOCompany model)
        {
            tblCompanyDetail company = new tblCompanyDetail();
            company.Name = model.Name;
            company.Phone = model.Phone;
            company.Address = model.Address;
            company.UserId = model.UserId;
            company.IsDeleted = false;

            company.Photo = model.Photo;

            company.Email = model.Email;
            company.Description = model.Description;
            company.CreatedAt = DateTime.Now.ToString();
            _db.tblCompanyDetails.Add(company);
            return _db.SaveChanges();
        }

        public int UpdateCompany(BOCompany model)
        {
            tblCompanyDetail company = _db.tblCompanyDetails.Where(u => u.Id == model.Id).FirstOrDefault();
            company.Name = model.Name;
            company.Phone = model.Phone;
            company.Address = model.Address;
            company.Photo = model.Photo;
            company.Email = model.Email;
            company.Description = model.Description;
            return _db.SaveChanges();
        }

        public List<BOCompany> GetAllCompany()
        {
            List<BOCompany> lst = new List<BOCompany>();
            var temp = _db.tblCompanyDetails.Where(u => u.IsDeleted == false).ToList();
            foreach (var model in temp)
            {
                BOCompany company = new BOCompany();
                company.Name = model.Name;
                company.Phone = model.Phone;
                company.Address = model.Address;
                company.Id = model.Id;
                company.UserId = model.UserId;
                company.Photo = model.Photo;
                company.Email = model.Email;
                company.Description = model.Description;
                lst.Add(company);
            }
            return lst;
        }

        public int DeleteCompany(int id)
        {
            tblCompanyDetail company = _db.tblCompanyDetails.Where(u => u.Id == id).FirstOrDefault();
            company.IsDeleted = true;
            //_db.tblCompanyDetails.Remove(company);
            return _db.SaveChanges();
        }

        public BOCompany GetCompanyById(int id)
        {
            tblCompanyDetail model = _db.tblCompanyDetails.Where(u => u.Id == id).FirstOrDefault();
            BOCompany company = new BOCompany();
            company.Name = model.Name;
            company.Phone = model.Phone;
            company.Address = model.Address;
            company.UserId = model.UserId;
            company.Photo = model.Photo;
            company.Email = model.Email;
            company.Description = model.Description;
            return company;
        }
    }
}
