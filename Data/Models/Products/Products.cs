using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebBuilder.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.Data.Models
{
    public class Products
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime launch { get; set; }

        [Required]
        public string title { get; set; }
        //[Required]
        //public string topDescription { get; set; }
        [Required]
        public string details { get; set; }
        [Required]
        public string overviews { get; set; }
        //[Required]
        //public string characterisc { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public bool isSpecial { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Companies Company { get; set; }
        [ForeignKey("CategoryId")]
        public Catergories Categories { get; set; }

        public ICollection<Images> Images { get; set; }

    }
}
