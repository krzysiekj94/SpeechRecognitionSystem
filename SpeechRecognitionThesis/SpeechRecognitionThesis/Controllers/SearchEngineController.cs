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
        private readonly IDataRepository<Article> _dataRepository;

        public SearchEngineController(IDataRepository<Article> dataRepository)
        {
            _dataRepository = dataRepository;
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
            return Json( _dataRepository.GetAll().ToList() );
        }

        [HttpGet("{name}")]
        public IActionResult GetArticle( string searchString )
        {
            return Json( _dataRepository.GetAll().ToList()
                .Where( articleNameString => articleNameString.ToString().Contains( searchString ) ) );
        }
    }
}