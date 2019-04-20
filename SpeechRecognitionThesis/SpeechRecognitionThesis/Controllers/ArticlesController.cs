using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SpeechRecognitionThesis.Controllers
{
    [Route("Articles")]
    public class ArticlesController : Controller
    {
        [Route("Newest")]
        public IActionResult GetNewestArticlesContent()
        {
            return View("Index");
        }

        [Route("Top-5")]
        public IActionResult GetTopArticlesContent()
        {
            return View("Article");
        }
    }
}