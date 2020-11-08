using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.Data.Models.User;

namespace WebBuilder.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string Photopath { get; set; }
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public virtual ICollection<UserStats> UserStats { get; set; }
        public virtual Companies Companies { get; set; }
    }
}
