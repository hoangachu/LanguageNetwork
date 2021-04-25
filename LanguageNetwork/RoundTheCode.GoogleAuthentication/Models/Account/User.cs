using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MulLan.Models.User
{
    public class User 
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProfileImg { get; set; }
        public string SeeLastestTime { get; set; }
        public string JoinToSystemTime { get; set; }
        public string Address { get; set; }
    }
   
}
