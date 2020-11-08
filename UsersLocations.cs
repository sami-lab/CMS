using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebBuilder.ViewModel.Administration;
using WebBuilder.ViewModel.Account;
using WebBuilder.Data.Interfaces;
using WebBuilder.Data.Models.User;
using WebBuilder.Data;

namespace WebBuilder
{
    public class UsersLocations
    {
    
        public async Task  readUserLocation(HttpContext context, ApplicationDbContext db)
        {
            UsersLocationViewModel location = new UsersLocationViewModel();
            string ip = readUserIP(context);
            location.IP = ip;
            location.Location = context.Request.Host + context.Request.Path + context.Request.QueryString;
            location.Browser =  context.Request.Headers["User-Agent"];
            //location.Url=
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new System.Net.WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
               
                if (ipInfo != null && string.IsNullOrEmpty(ipInfo.Country)) location.Location += " " + ipInfo.Country;
                if (ipInfo != null && string.IsNullOrEmpty(ipInfo.Region)) location.Location += " " + ipInfo.Region;
                if (ipInfo != null && string.IsNullOrEmpty(ipInfo.City)) location.Location += " " + ipInfo.City;
                if (ipInfo != null && string.IsNullOrEmpty(ipInfo.Loc)) location.Location += " " + ipInfo.Loc;
                if (ipInfo != null && string.IsNullOrEmpty(ipInfo.Timezone)) location.Location += " " + ipInfo.Timezone;

                //Save Data
                 await SaveLocation(location,db);

            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }

           
        }
        public string readUserIP(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress;
            //ip = System.Net.Dns.GetHostEntry(ip).AddressList
            //    .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            string result = "";
            if (ip != null)
            {
                // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
                // This usually only happens when the browser is on the same machine as the server.
                //if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                //{
                //    ip = System.Net.Dns.GetHostEntry(ip).AddressList
                //     .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                //}
                result = ip.ToString();
            }
          
            if (string.IsNullOrEmpty(result) || String.IsNullOrWhiteSpace(result))
                throw new Exception("Unable to determine caller's IP.");


            return result;
        }

        public async Task<int> SaveLocation(UsersLocationViewModel model,ApplicationDbContext db)
        {
            UsersLocation c = new UsersLocation()
            {
                IP = model.IP,
                Browser = model.Browser,
                Location = model.Location,
                Time = model.Time,
                Url = model.Url
            };

            db.UsersLocations.Add(c);
            await db.SaveChangesAsync();
            return c.id;
        }
    }
}
