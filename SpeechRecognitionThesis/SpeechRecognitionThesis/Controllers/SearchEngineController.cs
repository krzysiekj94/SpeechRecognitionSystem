using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognitionThesis.Models;
using SpeechRecognitionThesis.Models.Repository;
using SpeechRecognitionThesis.Models.ViewModels;

namespace SpeechRecognitionThesis.Controllers
{
    [Authorize]
    [Route("Search")]
    [ApiController]
    public class SearchEngineController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public SearchEngineController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetSearchEngineView()
        {
            SearchEngineModel searchEngineModel = new SearchEngineModel();

            searchEngineModel.ArticleCategoryList = _repositoryWrapper.ArticlesCategory
                                                    .FindAll().ToList();


            return View("Search", searchEngineModel );
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Results")]
        public IActionResult GetAllArticles()
        {
            List<Article> articleList = _repositoryWrapper.Articles.FindAll().ToList();
            articleList.ForEach( article => article.ArticleCategory = _repositoryWrapper.ArticlesCategory.GetCategory(article.ArticleCategoryRefId) );

            return Json( articleList );
        }

        [AllowAnonymous]
        [HttpGet("{name}")]
        public IActionResult GetArticle( string searchString )
        {
            return Json(_repositoryWrapper.Articles.FindAll().ToList()
                .Where( articleNameString => articleNameString.ToString().Contains( searchString ) ) );
        }
    }
}