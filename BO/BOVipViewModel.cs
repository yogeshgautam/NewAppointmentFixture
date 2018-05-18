using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BOVipViewModel
    {
        public int Id { get; set; }

        public Nullable<System.DateTime> Date { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public bool? IsCanceled { get; set; }

       
        public Nullable<int> DepartmentId { get; set; }
        public string AppointmentFrom { get; set; }
        public string AppointmentTo { get; set; }
        public string Details { get; set; }
        public string status { get; set; }
    }
}
