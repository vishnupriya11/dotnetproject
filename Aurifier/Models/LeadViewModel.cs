using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Aurifier.Models
{
    public class LeadViewModel
    {
        public class rolecontext
        {
            public DbSet<Role> role { get; set; }
            public DbSet<Registration> username { get; set; }
        }
        [Display(Name = "Lead_Id")]
        public int Lead_Id { get; set; }
        [Display(Name = "LeadName")]
        public string LeadName { get; set; }
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Mail_Id")]
        public string Mail_Id { get; set; }
        [Display(Name = "MobileNumber")]
        public string MobileNumber { get; set; }
        [Display(Name = "GeneratedBy")]
        public string GeneratedBy { get; set; }
        [Display(Name = "Team_Id")]
        public int Team_Id { get; set; }
        [Display(Name = "Manager")]
        public string Manager { get; set; }
        [Display(Name = "RolesId")]
        public int RolesId { get; set; }
        [Display(Name = "EmployeeId")]
        public int EmployeeId { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }
        [Display(Name = "Time")]
        [DataType(DataType.Time)]
        public System.TimeSpan Time { get; set; }
        public string Descreption { get; set; }
        public string Roles_Name { get; set; }
        public string TeamNumber { get; set; }
        public int EmpId { get; set; }
        public string UserName { get; set; }
        
    }

}