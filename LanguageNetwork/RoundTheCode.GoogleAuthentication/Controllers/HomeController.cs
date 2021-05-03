using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LanguageNetwork.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MulLan.Models.User;
using Newtonsoft.Json;
using RoundTheCode.GoogleAuthentication.Models;

namespace RoundTheCode.GoogleAuthentication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<HomeController> _logger;
        IHttpContextAccessor httpContextAccessor;
        //CookieValidatePrincipalContext context;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor HttpContextAccessor, IConfiguration config)
        {
            _logger = logger;
            httpContextAccessor = HttpContextAccessor;
            _config = config;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var currentuser = GetCurrentUser();
            if (currentuser != null)
            {
                return Redirect("Account/MainPage");
            }
            //if (currentuser != null)
            //{
            //    return Redirect("Account/ReponseMainPage");
            //}
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public User GetCurrentUser()
        {
            if (HttpContext != null)
            {
                var session = HttpContext.Session;
                string key_access = _config.GetValue<string>("Access_session");
                string json = session.GetString(key_access);
                if (json != null)
                {
                    return JsonConvert.DeserializeObject<User>(json);
                }
            }

            return null;
        }
    }
}
