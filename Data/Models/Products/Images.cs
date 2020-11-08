using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.Data.Models
{
    public class Images
    {
        [Key]
        public int id { get; set; }
        public string Image_Path { get; set; }
        [Required]
        public int productId { get; set; }


        [ForeignKey("productId")]
        public Products Product { get; set; }
    }
}
