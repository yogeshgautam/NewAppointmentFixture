using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BODateTime
    {
        public int Id { get; set; }
        public Nullable<int> AppointmentId { get; set; }

        [Required]
        public Nullable<System.DateTime> Date { get; set; }

        [Required]
        public string FromTime { get; set; }

        [Required]
        public string ToTime { get; set; }

      
        public Nullable<bool> IsCanceled { get; set; }
    }
}
