using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Account
{
    public class UserStatsViewModel
    {
        public int id { get; set; }
        [Display(Name = "Visits")]
        public int Count { get; set; }

        [Display(Name = "Time")]
        public DateTime When { get; set; }
        public string UserId { get; set; }
      
        public ApplicationUserViewModel User { get; set; }
    }
}
