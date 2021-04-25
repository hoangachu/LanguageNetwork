using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageNetwork.Controllers
{
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
