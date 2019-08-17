using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognitionThesis.Models;
using SpeechRecognitionThesis.Models.DatabaseModels;
using SpeechRecognitionThesis.Models.Repository;

namespace SpeechRecognitionThesis.Controllers
{
    [Authorize]
    [Route("Articles")]
    [ApiController]
    public class ArticlesController : Controller
    {
        private readonly IRespositoryWrapper _repositoryWrapper;

        public ArticlesController( IRespositoryWrapper respositoryWrapper )
        {
            _repositoryWrapper = respositoryWrapper;
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

        [AllowAnonymous]
        [HttpGet]
        [Route("Add")]
        public IActionResult GetAddArticleView()
        {
            return View("Add");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Add")]
        public IActionResult AddArticleItemToDb( [FromBody] Article article )
        {
            if( article == null )
            {
                return BadRequest("Article is null.");
            }

            long iLoggedUserId = -1;

            if( HttpContext.User.Identity.IsAuthenticated )
            {
                iLoggedUserId = GetLoggedUserId();

                if( iLoggedUserId > -1 )
                {
                    SaveNewUserArticle( iLoggedUserId, article );
                }
            }
            else
            {
                SaveNewGuestArticle( article );
            }

            return Ok();
        }

        private void SaveNewGuestArticle( Article article )
        {
            User anonymousUser = _repositoryWrapper.Account.GetAnonymousUser();

            if( anonymousUser != null )
            {
                SaveNewUserArticle( anonymousUser.Id, article );
            }
        }

        private long GetLoggedUserId()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            long iLoggedUserId = -1;

            if( !long.TryParse(identity.FindFirst("UserId").Value, out iLoggedUserId ) )
            {
                iLoggedUserId = -1;
            }

            return iLoggedUserId;
        }

        private void SaveNewUserArticle( long iUserId, Article article )
        {
            User user = _repositoryWrapper.Account.GetUser( iUserId );

            if( user != null )
            {
                _repositoryWrapper.Articles.Add( article );
                _repositoryWrapper.Save(); //After save article object is filled correctly ID from DB
                _repositoryWrapper.UserArticles.Add( GetNewUserArticle( user, article ) );
                _repositoryWrapper.Save();
            }
        }

        private UserArticles GetNewUserArticle(User user, Article article)
        {
            UserArticles userArticles = new UserArticles();

            userArticles.ArticleRefId = article.Id;
            userArticles.UserRefId = user.Id;
            userArticles.ArticleModificationDate = DateTime.Now.ToString();

            return userArticles;
        }
    }
}