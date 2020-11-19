using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Categories
{
    public class CategoriesViewModel
    {
        public int id { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime launch { get; set; }
        [Required]
        [Display(Name = "分类名称")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "公司")]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        [Display(Name = "产品总数")]
        public int TotalProducts { get; set; }
    }
}
