using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.Data.Models.Contact
{
    public class Contact
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }

        public int? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Companies Company { get; set; }

    }
}
