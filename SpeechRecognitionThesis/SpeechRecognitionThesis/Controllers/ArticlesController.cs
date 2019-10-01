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
            NewestArticlesModel newestArticleModel = new NewestArticlesModel();
            List<NewestArticle> newestArticlesList = GetNewestArticlesList();

            newestArticleModel.NewestArticleList = newestArticlesList;

            return View( "Newest", newestArticleModel );
        }

        private List<NewestArticle> GetNewestArticlesList()
        {
            const int iNumberOfArticlesResult = 10;
            List<NewestArticle> newestArticleList = new List<NewestArticle>();
            List<Article> newestArticles = _repositoryWrapper.Articles.GetNewestArticles( iNumberOfArticlesResult );
            UserArticles userArticle = null;
            User user = null;

            foreach( var article in newestArticles )
            {
                article.ArticleCategory = _repositoryWrapper.ArticlesCategory.GetCategory( article.ArticleCategoryRefId );
                userArticle = _repositoryWrapper.UserArticles.GetUserArticle( article );

                if( userArticle != null )
                {
                    user = _repositoryWrapper.Account.GetUser( userArticle.UserRefId );

                    if( user != null )
                    {
                        newestArticleList.Add(new NewestArticle()
                        {
                            Article = article,
                            User = user
                        });
                    }
                }
            }

            return newestArticleList;
        }

        private User GetUserFromArticle( Article article )
        {
            UserArticles userArticle = _repositoryWrapper.UserArticles.GetUserArticle( article );
            User userFromArticle = _repositoryWrapper.Account.GetUser( userArticle.UserRefId );

            return userFromArticle;
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

        [AllowAnonymous]
        [HttpGet]
        [Route("{lArticleId}")]
        public IActionResult GetArticleView( [FromRoute] long lArticleId)
        {
            ConcreteArticleModel concreteArticleModel = new ConcreteArticleModel();
            FillConcreteArticleModel( concreteArticleModel, lArticleId, -1 );

            return View( "Article", concreteArticleModel );
        }

        private void FillConcreteArticleModel(ConcreteArticleModel concreteArticleModel, long lArticleId, long lUserId )
        {
            Article conreteArticle = _repositoryWrapper.Articles.GetArticle(lArticleId);
            UserArticles concreteUserArticle = new UserArticles();

            if(lUserId > -1 )
            {
                concreteUserArticle = _repositoryWrapper.UserArticles
                                    .GetUserArticle(lArticleId, lUserId);
            }
            else
            {
                concreteUserArticle = _repositoryWrapper.UserArticles
                                   .GetUserArticle(lArticleId);
            }

            conreteArticle.ArticleCategory = _repositoryWrapper.ArticlesCategory
                                            .GetCategory(conreteArticle.ArticleCategoryRefId);
            if( lUserId > -1 )
            {
                concreteUserArticle.User = _repositoryWrapper.Account.GetUser(lUserId);
            }
            else
            {
                concreteUserArticle.User = _repositoryWrapper.Account.GetUser(concreteUserArticle.UserRefId);
            }

            concreteUserArticle.User.Password = "";

            concreteArticleModel.Article = conreteArticle;
            concreteArticleModel.UserArticle = concreteUserArticle;
        }

        [HttpGet]
        [Route("{lArticleId}/edit")]
        public IActionResult GetEditArticleView( [FromRoute] long lArticleId )
        {
            EditConcreteArticleModel editConcreteArticleModel = new EditConcreteArticleModel();
            ConcreteArticleModel concreteArticleModel = new ConcreteArticleModel();
            editConcreteArticleModel.ConcreteArticleModel = concreteArticleModel;
            FillConcreteArticleModel(editConcreteArticleModel.ConcreteArticleModel, lArticleId, GetLoggedUserId() );
            editConcreteArticleModel.ArticleCategoryList = _repositoryWrapper.ArticlesCategory.FindAll().ToList();


            return View( "Edit", editConcreteArticleModel );
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        [Route("{lArticleId}")]
        public IActionResult UpdateUserArticle( [FromRoute] long lArticleId, [FromBody] Article article )
        {
            if( article == null || lArticleId < 0)
            {
                return BadRequest("Article is null.");
            }

            long iLoggedUserId = -1;

            if( HttpContext.User.Identity.IsAuthenticated )
            {
                iLoggedUserId = GetLoggedUserId();

                if( iLoggedUserId > -1 )
                {
                    UpdateUserArticleDb( iLoggedUserId, lArticleId, article );
                }
            }
            else
            {
                return BadRequest("Article for guest will be not editable!");
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{lArticleId}")]
        public IActionResult DeleteUserArticle([FromRoute] long lArticleId )
        {
            if( lArticleId < 0)
            {
                return BadRequest("Article is null.");
            }

            long iLoggedUserId = -1;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                iLoggedUserId = GetLoggedUserId();

                if( iLoggedUserId > -1 )
                {
                    DeleteUserArticleDb( iLoggedUserId, lArticleId );
                }
            }
            else
            {
                return BadRequest("Article for guest will be not editable!");
            }

            return Ok();
        }

        private void DeleteUserArticleDb( long lLoggedUserId, long lArticleId )
        {
            User user = _repositoryWrapper.Account.GetUser(lLoggedUserId);
            Article articleFromDb = _repositoryWrapper.Articles.GetArticle(lArticleId);
            UserArticles userArticleFromDb = _repositoryWrapper.UserArticles.GetUserArticle(articleFromDb.Id, user.Id);

            if( user != null && articleFromDb != null && userArticleFromDb != null )
            {
                _repositoryWrapper.UserArticles.Delete(userArticleFromDb);
                _repositoryWrapper.Articles.Delete(articleFromDb);
                _repositoryWrapper.Save();
            }
        }

        private void UpdateUserArticleDb( long lLoggedUserId, long lArticleId, Article article )
        {
            User user = _repositoryWrapper.Account.GetUser( lLoggedUserId );
            Article articleFromDb = _repositoryWrapper.Articles.GetArticle( lArticleId );
            bool bIsChange = false;

            if ( user != null && articleFromDb != null )
            {
                if( article.ArticleCategoryRefId > -1 
                    && articleFromDb.ArticleCategoryRefId != article.ArticleCategoryRefId )
                {
                    articleFromDb.ArticleCategoryRefId = article.ArticleCategoryRefId;
                    bIsChange = true;
                }

                if( article.Subject != null
                    && article.Subject.Length > 0 
                    && article.Subject != articleFromDb.Subject )
                {
                    articleFromDb.Subject = article.Subject;
                    bIsChange = true;
                }

                if (article.Content != null
                    && article.Content.Length > 0
                    && article.Content != articleFromDb.Content)
                {
                    articleFromDb.Content = article.Content;
                    bIsChange = true;
                }

                if( bIsChange )
                {
                    articleFromDb.ArticleModificationDate = DateTime.Now.ToString();
                    _repositoryWrapper.Articles.Update( articleFromDb );
                    _repositoryWrapper.Save();
                }
            }
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