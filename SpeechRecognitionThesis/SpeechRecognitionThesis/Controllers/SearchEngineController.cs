﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognitionThesis.Models;
using SpeechRecognitionThesis.Models.Repository;

namespace SpeechRecognitionThesis.Controllers
{
    [Authorize]
    [Route("Search")]
    [ApiController]
    public class SearchEngineController : Controller
    {
        private readonly IRespositoryWrapper _repositoryWrapper;

        public SearchEngineController(IRespositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetSearchEngineView()
        {
            return View("Search");
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