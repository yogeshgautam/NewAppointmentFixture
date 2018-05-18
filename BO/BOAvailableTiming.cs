using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BOAvailableTiming
    {
        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }

        [Required]
        public Nullable<bool> IsAvailable { get; set; }
        public Nullable<int> VipId { get; set; }
    }
}
