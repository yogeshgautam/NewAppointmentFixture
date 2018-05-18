using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BOVIPTable
    {

        public BOVIPTable()
        {
            Photo = "profile.png";
        }
        public int Id { get; set; }

 
        [Required]
        public Nullable<int> DepartmentId { get; set; }
        public string UserId { get; set; }

     
        [Required]
        public string Designation { get; set; }

        [Required]
        public string Phone { get; set; }

    
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        [Required]
        public string Details { get; set; }

      
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Photo { get; set; }
        public string CreatedAt { get; set; }
        public string LastUpdated { get; set; }


        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public BODepartment lstDepartment { get; set; }
    }
}
