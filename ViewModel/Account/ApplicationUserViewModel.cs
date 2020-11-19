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
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        //[Remote(action: "IsEmailexist", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password",
            ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "电话")]
        //[RegularExpression(@"^[0][1-9]\d{9}$|^[1-9]\d{9}$", ErrorMessage = "Must be Phone No")]
        public string PhoneNumber { get; set; }

        [Display(Name = "头像照片")]
        public IFormFile Photo { get; set; }
        [Display(Name = "头像照片")]
        public string Photopath { get; set; }

        [Display(Name = "今天访问")]
        public int TotaldailyVisits { get; set; }
        [Display(Name = "上个月访问")]
        public int TotalMonthlyVisits { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
