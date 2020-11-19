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
        [Display(Name= "名称")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Required]
        //[RegularExpression(@"^[0][1-9]\d{9}$|^[1-9]\d{9}$", ErrorMessage = "Must be Phone No")]
        [Display(Name = "电话")]
        public string Phone { get; set; }

        [Display(Name = "学科")]
        public string Subject { get; set; }
        [Display(Name = "信息")]
        public string Message { get; set; }

        public int? CompanyId { get; set; }

    }
}
