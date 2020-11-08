using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.Data.Models
{
    public class Address
    {
        [Key]
        public int id { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        [Display(Name = "Complete Street Adress")]
        public string StreetAddress { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
