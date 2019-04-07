using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SpeechRecognitionThesis.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult ArticleView()
        {
            return View();
        }
    }
}