using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapstoneProject_Ecommerce.Models
{
    public class Email
    {
        [Required]
        [Display (Name="Email Mittente")]
        public string mittenteEmail { get; set; }

        [Required]

        public string Oggetto { get; set; }

        [Required]

        public string Messaggio { get; set; }
    }    
}