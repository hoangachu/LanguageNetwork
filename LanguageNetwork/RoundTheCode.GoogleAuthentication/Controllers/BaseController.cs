using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MulLan.Models.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageNetwork.Controllers
{
    public class BaseController : Controller
    {
        public static int UserID;
        
        public BaseController(IHttpContextAccessor _httpcontextaccessor, IConfiguration _config)
        {
            ViewBag.CurrentUser = 0;
            if (_httpcontextaccessor.HttpContext != null)
            {
                var session = _httpcontextaccessor.HttpContext.Session;
                string key_access = _config.GetValue<string>("Access_session");
                string json = session.GetString(key_access);
                if(json != null)
                {
                    var user = JsonConvert.DeserializeObject<User>(json);
                    if (user != null)
                    {
                        UserID = user.UserID;
                        ViewBag.CurrentUser = 1;
                    }
                }
               
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
