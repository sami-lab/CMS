using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.Data.Models
{
    public class Catergories
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime launch { get; set; }

        [Required]
        public string CategoryName { get; set; }
        [Required]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Companies Company { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
