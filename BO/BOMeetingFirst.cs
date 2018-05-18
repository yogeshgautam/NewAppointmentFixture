using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
  public  class BOMeetingFirst
    {
        public int ID { get; set; }

        [Required]
        public string VIPuser { get; set; }

        [Required]
        public string Users { get; set; }

        public string VipName { get; set; }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }

        [Required]
        public string Date { get; set; }
    }
}
