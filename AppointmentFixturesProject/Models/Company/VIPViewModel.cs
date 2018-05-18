using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentFixturesProject.Models.Company
{
    public class VIPViewModel
    {
        public int Id { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Designation { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }

        public virtual List<BO.BODepartment> departments { get; set; }
    }
}