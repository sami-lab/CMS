using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.Data.Models.User
{
    public class UserStats
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Visits")]
        public int Count { get; set; }

        [Display(Name = "Time")]
        public DateTime When { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
      
    }


}
