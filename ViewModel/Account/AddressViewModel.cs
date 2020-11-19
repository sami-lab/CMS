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
        [Display(Name = "国家")]
        [Required]
        public int CountryId { get; set; }
        [Display(Name = "国家")]

        public string Country { get; set; }
        [Display(Name = "州")]
        [Required]
        public int StateId { get; set; }
        [Display(Name = "州")]
        public string State { get; set; }
        [Required]
        [Display(Name = "市")]
        public int CityId { get; set; }
        [Display(Name = "市")]
        public string City { get; set; }
        [Display(Name = "完整的街道地址")]
        [Required]
        public string StreetAddress { get; set; }
    }
}
