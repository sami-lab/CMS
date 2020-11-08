using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Account
{
    public class AddressViewModel
    {

        public int id { get; set; }
        [Display(Name = "Country")]
        [Required]
        public int CountryId { get; set; }
        [Display(Name = "Country")]

        public string Country { get; set; }
        [Display(Name = "State")]
        [Required]
        public int StateId { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        public int CityId { get; set; }
        public string City { get; set; }
        [Display(Name = "Complete Street Adress")]
        [Required]
        public string StreetAddress { get; set; }
    }
}
