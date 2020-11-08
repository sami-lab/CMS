using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Account
{
    public class UsersLocationViewModel
    {
       
        public int id { get; set; }

        [Display(Name = "IP")]
        public string IP { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Access Time")]
        public DateTime Time { get; set; }

        [Display(Name = "URL")]
        public string Url { get; set; }


        [Display(Name = "Location")]
        public string Browser { get; set; }
    }
}
