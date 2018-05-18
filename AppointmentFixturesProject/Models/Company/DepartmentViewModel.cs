using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentFixturesProject.Models.Company
{
    public class DepartmentViewModel
    {
        public int id { get; set; }

        public int CompanyId { get; set; }

        public string DepartmentName { get; set; }

    }
}