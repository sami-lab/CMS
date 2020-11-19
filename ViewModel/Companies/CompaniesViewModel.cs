using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Companies
{
    public class CompaniesViewModel
    {
        public CompaniesViewModel()
        {
            Products = new List<Products.ProductsViewModel>();
            SpecialProducts = new List<Products.ProductsViewModel>();
            Categories = new List<Categories.CategoriesViewModel>();
        }
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "日期")]
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }

        [Required]
        [Display(Name = "公司名")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "公司名称")]
        public string CompanyTitle { get; set; }
        [Required]
        [Display(Name = "公司介绍")]
        public string CompanyDesc { get; set; }

        [Display(Name = "商标")]
        public string CompanyLogoPath { get; set; } //image

     
        [DataType(DataType.Upload)]
        [Display(Name = "商标")]
        public IFormFile CompanyLogo { get; set; } //image

        [Display(Name = "背景图")]
        public string CompanyBackgorundPath { get; set; } //image

      
        [DataType(DataType.Upload)]
        [Display(Name = "背景图")]
        public IFormFile CompanyBackgorund { get; set; } //image

        [Display(Name = "Why Choose Us Image")]
        public string whyCooseUsImagePath { get; set; } //image

        
        [DataType(DataType.Upload)]
        public IFormFile whyCooseUsImage { get; set; } //image
        [Required]
        [Display(Name = "为什么选择我们文字")]
        public string whyCooseUsText { get; set; }

        [Required]
        [Display(Name = "公司完整地址")]
        public string CompanyAdd { get; set; }
        [Required]
        [Display(Name = "公司电话")]
        public string CompanyPhone { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "公司电邮")]
        public string CompanyEmail { get; set; }
        
        public string CompanyOwner { get; set; }
        [Display(Name = "所有者名称")]
        public string CompanyOwnerName { get; set; }
        public string CompanyOwnerImage { get; set; }

        [Display(Name = "Facebook个人资料")]
        public string FbProfile { get; set; }

        [Display(Name = "Twitter 个人资料")]
        public string twitterProfile { get; set; }

        [Display(Name = "LinkedIn 个人资料")]
        public string linkedinProfile { get; set; }

        public int TotalCategories { get; set; }
        public int TotalProducts { get; set; }
        public  List<Products.ProductsViewModel> Products{ get; set;}
        public List<Products.ProductsViewModel> SpecialProducts { get; set; }
        public List<Categories.CategoriesViewModel> Categories { get; set; }
    }
}
