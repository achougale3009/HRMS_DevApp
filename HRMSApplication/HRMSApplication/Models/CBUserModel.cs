using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HRMSApplication.Models
{
    public class CBUserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]        
        public string Password { get; set; }
    }
}