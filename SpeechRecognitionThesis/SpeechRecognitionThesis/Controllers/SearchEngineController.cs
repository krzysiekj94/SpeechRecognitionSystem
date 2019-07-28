using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognitionThesis.Models;
using SpeechRecognitionThesis.Models.Repository;

namespace SpeechRecognitionThesis.Controllers
{
    [Route("Search")]
    [ApiController]
    public class SearchEngineController : Controller
    {
        private readonly IRespositoryWrapper _repositoryWrapper;

        public SearchEngineController(IRespositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult GetSearchEngineView()
        {
            return View("Search");
        }

        [HttpGet]
        [Route("Results")]
        public IActionResult GetAllArticles()
        {
            return Json(_repositoryWrapper.Articles.FindAll().ToList() );
        }

        [HttpGet("{name}")]
        public IActionResult GetArticle( string searchString )
        {
            return Json(_repositoryWrapper.Articles.FindAll().ToList()
                .Where( articleNameString => articleNameString.ToString().Contains( searchString ) ) );
        }
    }
}