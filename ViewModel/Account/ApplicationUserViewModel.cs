using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Account
{
    public class ApplicationUserViewModel
    {
      
        public string Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        //[Remote(action: "IsEmailexist", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone")]
        //[RegularExpression(@"^[0][1-9]\d{9}$|^[1-9]\d{9}$", ErrorMessage = "Must be Phone No")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Profile Photo")]
        public IFormFile Photo { get; set; }
        [Display(Name = "Profile Photo")]
        public string Photopath { get; set; }

        [Display(Name = "Today Visits")]
        public int TotaldailyVisits { get; set; }
        [Display(Name = "Last Month Visits")]
        public int TotalMonthlyVisits { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
