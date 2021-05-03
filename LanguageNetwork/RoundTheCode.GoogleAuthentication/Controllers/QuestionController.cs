using LanguageNetwork.Models.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoundTheCode.GoogleAuthentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageNetwork.Controllers
{
    //[Authorize]
    //[ValidateAntiForgeryToken]
    [Route("Question")]
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Ask")]
        public IActionResult Ask()
        {
            
            return View();
        }
    }
}
