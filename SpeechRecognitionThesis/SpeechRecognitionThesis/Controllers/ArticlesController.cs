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
using SpeechRecognitionThesis.Models.Scripts;
using SpeechRecognitionThesis.Models.ViewModels;

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

        [AllowAnonymous]
        [HttpGet]
        [Route("Newest")]
        public IActionResult GetNewestArticlesContent()
        {
            return View("Newest");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Top-5")]
        public IActionResult GetTopArticlesContent()
        {
            return View("Top");
        }

        [AllowAnonymous]
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
            ArticleUserModel articleUserModel = new ArticleUserModel();
            articleUserModel.ArticleCategoryList = _repositoryWrapper.ArticlesCategory.FindAll().ToList();

            return View("Add", articleUserModel);
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

        [HttpGet]
        [Route("My")]
        public IActionResult GetMyArticleView()
        {
            MyArticlesModel myArticlesModel = new MyArticlesModel();
            SetLoggedUserForAccountUserModel(myArticlesModel);

            return View("My", myArticlesModel);
        }

        [HttpGet]
        [Route("{articleId}")]
        public IActionResult GetArticleView( [FromRoute] int articleId )
        {
            return View("Article");
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
                 article.ArticleModificationDate = DateTime.Now.ToString();
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

            return userArticles;
        }

        private void SetLoggedUserForAccountUserModel(MyArticlesModel myArticlesModel)
        {
            long lUserId = GetLoggedUserId();
            User loggedUser = null;
            IEnumerable<UserArticles> userArticlesList = null;
            List<Article> articleList = null;

            if (myArticlesModel != null
                && lUserId > -1 )
            {
                loggedUser = _repositoryWrapper.Account.GetUser(lUserId);
                loggedUser.Password = string.Empty;
                loggedUser.Id = -1;

                myArticlesModel.User = loggedUser;
                userArticlesList = _repositoryWrapper.UserArticles.GetUserArticles(lUserId);

                if(userArticlesList != null 
                    && userArticlesList.Count() > 0)
                {
                    articleList = _repositoryWrapper.Articles.GetArticles(userArticlesList);
                    SetCategoryForUserArticles(articleList);
                    myArticlesModel.UserArticleList = articleList;
                }
            }
        }

        private void SetCategoryForUserArticles(List<Article> articleList)
        {
            for( int i = 0; i < articleList.Count; i++)
            {
                articleList[i].ArticleCategory = _repositoryWrapper.ArticlesCategory.GetCategory(articleList[i].ArticleCategoryRefId);
            }
        }
    }
}