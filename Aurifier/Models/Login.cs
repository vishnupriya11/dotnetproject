using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aurifier.Models
{
    public class Login
    {

        [Required(ErrorMessage = "Please Provide Email", AllowEmptyStrings = false)]
        public String Email { get; set; }
        [Required(ErrorMessage = "Please Provide Password", AllowEmptyStrings = false)]
        public String Password { get; set; }
    }


}