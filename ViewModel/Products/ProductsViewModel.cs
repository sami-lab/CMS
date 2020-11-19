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
        [Display(Name = "产品名称")]
        public string title { get; set; }
       
        [Required]
        [Display(Name = "细节")]
        public string details { get; set; }
        [Required]
        [Display(Name = "概述和说明")]
        public string overviews { get; set; }
      
        [Required]
        [Display(Name = "类别")]
        public int CategoryId { get; set; }
        [Display(Name = "类别")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "特别 ?")]
        public bool isSpecial { get; set; }

        //Company Data
        [Required]
        [Display(Name = "公司")]
        public int CompanyId { get; set; }
        [Display(Name = "公司")]
        public string CompanyName { get; set; }
        [Display(Name = "公司介绍")]
        public string CompanyDesc { get; set; }
        [Display(Name = "公司名称")]
        public string CompanyTitle { get; set; }
        [Display(Name = "公司标志")]
        public string CompanyLogoPath { get; set; }
        [Display(Name = "公司地址")]
        public string CompanyAdd { get; set; }
        [Display(Name = "公司电话")]
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string FbProfile { get; set; }
        public string twitterProfile { get; set; }
        public string linkedinProfile { get; set; }



        public List<IFormFile> Photos { get; set; }
        public List<ImagesViewModel> Images { get; set; }
        [Display(Name = "图片")]
        public string ImageName { get; set; }

    }
}
