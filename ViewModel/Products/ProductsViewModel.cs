using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Products
{
    public class ProductsViewModel
    {
        public ProductsViewModel()
        {
            Images = new List<ImagesViewModel>();
        }
        [DataType(DataType.Date)]
        public System.DateTime launch { get; set; }

        [Display(Name = "ID")]
        public int id { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string title { get; set; }
       
        [Required]
        [Display(Name = "Details")]
        public string details { get; set; }
        [Required]
        [Display(Name = "Overview and Description")]
        public string overviews { get; set; }
      
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Special?")]
        public bool isSpecial { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        [Display(Name = "Company")]
        public string CompanyName { get; set; }
        [Display(Name = "Company Description")]
        public string CompanyDesc { get; set; }
        [Display(Name = "Company Title")]
        public string CompanyTitle { get; set; }
        [Display(Name = "Company Logo")]
        public string CompanyLogoPath { get; set; }
        [Display(Name = "Company Add")]
        public string CompanyAdd { get; set; }
        [Display(Name = "Company Phone")]
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }

        public List<IFormFile> Photos { get; set; }
        public List<ImagesViewModel> Images { get; set; }
        [Display(Name = "Image")]
        public string ImageName { get; set; }

    }
}
