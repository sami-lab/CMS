using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Products
{
    public class ImagesViewModel
    {
        public int id { get; set; }
        public string Image_Path { get; set; }
        [Required]
        public int productId { get; set; }
    }
}
