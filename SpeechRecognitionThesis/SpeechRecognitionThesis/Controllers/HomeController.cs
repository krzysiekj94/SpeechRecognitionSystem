using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    public class HomeController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public HomeController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            IndexArticlesModel indexArticlesModel = new IndexArticlesModel();
            FillIndexArticlesModel( indexArticlesModel );

            return View( indexArticlesModel );
        }

        private void FillIndexArticlesModel(IndexArticlesModel indexArticlesModel)
        {
            if( indexArticlesModel != null )
            {
                indexArticlesModel.CurrentUser = GetUserForIndex();
                indexArticlesModel.NewestArticleUserPairList = GetNewestArticlesForIndex();
                indexArticlesModel.MostViewedUserArticlePairList = GetMostViewedArticlesForIndex();
                indexArticlesModel.MyArticleList = GetMyArticleArticlesForIndex( indexArticlesModel.CurrentUser );
            }
        }

        private List<Article> GetMyArticleArticlesForIndex( User concreteUser )
        {
            List<Article> myArticleList = null;
            IEnumerable<UserArticles> userArticleEnumerable = null;

            if( IsCorrectUser( concreteUser ) )
            {
                userArticleEnumerable = _repositoryWrapper.UserArticles.GetUserArticles( concreteUser.Id );

                if(userArticleEnumerable != null 
                    && userArticleEnumerable.Count() > 0 )
                {
                    myArticleList = _repositoryWrapper.Articles.GetArticles( userArticleEnumerable );
                }
            }

            return myArticleList != null ? myArticleList : new List<Article>();
        }

        private bool IsCorrectUser(User concreteUser)
        {
            return concreteUser != null
                && ( (concreteUser.Id > -1)
                       || ( concreteUser.Id == -1
                            && concreteUser.NickName == UserTools.ANONYMOUS_USER_NICKNAME) );
        }

        private List<ArticleUserPair> GetMostViewedArticlesForIndex()
        {
            const int iNumberOfArticleResult = 3;

            List<Article> mostViewedArticleList = _repositoryWrapper.Articles
                                                  .GetMostViewedArticles( iNumberOfArticleResult );

            List<ArticleUserPair> mostViewedArticleUserPairList = UserTools.GetUserArticlePair(_repositoryWrapper, mostViewedArticleList);


            return (mostViewedArticleUserPairList != null ) ? mostViewedArticleUserPairList : new List<ArticleUserPair>();
        }

        private List<ArticleUserPair> GetNewestArticlesForIndex()
        {
            const int iNumberOfArticleResult = 3;
            List<Article> newestArticleList = _repositoryWrapper.Articles
                                              .GetNewestArticles( iNumberOfArticleResult );
            List<ArticleUserPair> newestArticleUserPairList = UserTools.GetUserArticlePair(_repositoryWrapper, newestArticleList);

            return ( newestArticleUserPairList != null ) ? newestArticleUserPairList : new List<ArticleUserPair>();
        }


        private List<ArticleUserPair> GetNewestArticlesList()
        {
            const int iNumberOfArticlesResult = 10;
            List<Article> newestArticles = _repositoryWrapper.Articles.GetNewestArticles(iNumberOfArticlesResult);
            List<ArticleUserPair> newestArticleList = UserTools.GetUserArticlePair(_repositoryWrapper, newestArticles);

            return newestArticleList;
        }

        private User GetUserForIndex()
        {
            long lUserId = -1;
            User currentUser = null;

            if( long.TryParse(TokenProvider.GetRegisterUserPropertyString(User.Identity, UserTools.USER_ID_PROPERTY_STRING), out lUserId))
            {
                currentUser = _repositoryWrapper.Account.GetUser(lUserId);
            }
            else
            {
                currentUser = _repositoryWrapper.Account.GetAnonymousUser();
            }

            currentUser.Password = string.Empty;
            currentUser.Id = -1;

            return currentUser;
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "Opis mojej aplikacji";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
