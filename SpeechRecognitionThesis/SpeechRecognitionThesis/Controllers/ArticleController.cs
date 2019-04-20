using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SpeechRecognitionThesis.Controllers
{
    [Route("Article")]
    public class ArticleController : Controller
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        } 
    }
}