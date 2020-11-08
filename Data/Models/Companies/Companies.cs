using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.Data.Models
{
    public class Companies
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }

        [Required]
        public string CompanyName { get; set; }
        [Required] //Top section
        public string CompanyTitle { get; set; }
        [Required]
        public string CompanyDesc { get; set; }
        [Required]
        public string CompanyLogo { get; set; } //image
        [Required]
        public string CompanyBackgorund { get; set; } //image
        [Required]
        public string whyCooseUsImage { get; set; } //image
        [Required]
        public string whyCooseUsText { get; set; }

        [Required]
        public string CompanyAdd { get; set; } 
        [Required]
        public string CompanyPhone { get; set; }
        [Required]
        public string CompanyEmail { get; set; }
        [Required]
        public string CompanyOwner { get; set; } //User

        [ForeignKey("CompanyOwner")]
        public ApplicationUser User { get; set; }

        public virtual ICollection<Contact.Contact> Contacts { get; set; }
        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<Catergories> Catergories { get; set; }
    }
}
