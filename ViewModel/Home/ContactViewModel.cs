using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Home
{
    public class ContactViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Name*")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email*")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone*")]
        //[RegularExpression(@"^[0][1-9]\d{9}$|^[1-9]\d{9}$", ErrorMessage = "Must be Phone No")]
        public string Phone { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }

        public int? CompanyId { get; set; }

    }
}
