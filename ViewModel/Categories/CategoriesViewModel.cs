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
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        [Display(Name = "Total Products")]
        public int TotalProducts { get; set; }
    }
}
