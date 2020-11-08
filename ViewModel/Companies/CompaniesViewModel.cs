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

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Company Title")]
        public string CompanyTitle { get; set; }
        [Required]
        [Display(Name = "Company Description")]
        public string CompanyDesc { get; set; }

        [Display(Name = "Logo")]
        public string CompanyLogoPath { get; set; } //image

     
        [DataType(DataType.Upload)]
        [Display(Name = "Logo")]
        public IFormFile CompanyLogo { get; set; } //image

        [Display(Name = "Background Image")]
        public string CompanyBackgorundPath { get; set; } //image

      
        [DataType(DataType.Upload)]
        [Display(Name = "Background Image")]
        public IFormFile CompanyBackgorund { get; set; } //image

        [Display(Name = "Why Choose Us Image")]
        public string whyCooseUsImagePath { get; set; } //image

        
        [DataType(DataType.Upload)]
        public IFormFile whyCooseUsImage { get; set; } //image
        [Required]
        [Display(Name = "Why Choose Us Text")]
        public string whyCooseUsText { get; set; }

        [Required]
        [Display(Name = "Company Complete Address")]
        public string CompanyAdd { get; set; }
        [Required]
        [Display(Name = "Company Phone")]
        public string CompanyPhone { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Company Email")]
        public string CompanyEmail { get; set; }
        
        public string CompanyOwner { get; set; }
        [Display(Name = "Owner Name")]
        public string CompanyOwnerName { get; set; }
        public string CompanyOwnerImage { get; set; }

        public int TotalCategories { get; set; }
        public int TotalProducts { get; set; }
        public  List<Products.ProductsViewModel> Products{ get; set;}
        public List<Products.ProductsViewModel> SpecialProducts { get; set; }
        public List<Categories.CategoriesViewModel> Categories { get; set; }
    }
}
