using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aurifier.Models
{
    public class EmployeeViewModel
    {
        
        [Display(Name = "Id")]
        public int EmpId { get; set; }
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Role")]
        public int Roles_Id { get; set; }
        [Display(Name = "Team")]
        public int TeamId { get; set; }
        public string Roles_Name { get; set; }
        public string TeamNumber { get; set; }
    }
}