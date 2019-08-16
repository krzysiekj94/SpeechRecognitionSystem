using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognitionThesis.Models;
using SpeechRecognitionThesis.Models.Repository;

namespace SpeechRecognitionThesis.Controllers
{
    [Route("Articles")]
    [ApiController]
    public class ArticlesController : Controller
    {
        private readonly IDataRepository<Article> _dataRepository;

        public ArticlesController(IDataRepository<Article> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        [Route("Newest")]
        public IActionResult GetNewestArticlesContent()
        {
            return View("Newest");
        }

        [HttpGet]
        [Route("Top-5")]
        public IActionResult GetTopArticlesContent()
        {
            return View("Top");
        }

        [HttpGet]
        [Route("Recommended")]
        public IActionResult GetRecommendedArticlesContent()
        {
            return View("Recommended");
        }

        [HttpGet]
        [Route("Add")]
        public IActionResult GetAddArticleView()
        {
            return View("Add");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Add")]
        public IActionResult AddArticleItemToDb( [FromBody] Article article )
        {
            if( article == null )
            {
                return BadRequest("Article is null.");
            }

            _dataRepository.Add( article );

            return Ok( article.Id );
        }
    }
}