//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblVIPUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblVIPUser()
        {
            this.tblAvailableTimings = new HashSet<tblAvailableTiming>();
        }
    
        public int Id { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string UserId { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string Details { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public string CreatedAt { get; set; }
        public string LastUpdated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAvailableTiming> tblAvailableTimings { get; set; }
        public virtual tblDepartment tblDepartment { get; set; }
    }
}
