using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBuilder.ViewModel.Administration
{
    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("timezone")]
        [Display(Name = "时区")]
        public string Timezone { get; set; }

        [JsonProperty("city")]
        [Display(Name = "时区")]
        public string City { get; set; }

        [JsonProperty("region")]
        [Display(Name = "地区")]
        public string Region { get; set; }

        [JsonProperty("country")]
        [Display(Name = "国家")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        [Display(Name = "位置")]
        public string Loc { get; set; }

        [JsonProperty("org")]
        [Display(Name = "组织")]
        public string Org { get; set; }

        [JsonProperty("postal")]
        [Display(Name = "邮政")]
        public string Postal { get; set; }
    }
}
