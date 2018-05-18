using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BOCompany
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Photo { get; set; }
        public string UserId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Phone { get; set; }
        public string CreatedAt { get; set; }
        public string LastUpdated { get; set; }


        [Required]
        public string password { get; set; }

        [Compare("password")]
        public string ConfirmPassword { get; set; }

        public BOCompany()
        {
            Photo = "default.png";
        }
    }
}
