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

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        private void FillIndexArticlesModel(IndexArticlesModel indexArticlesModel)
        {
            if( indexArticlesModel != null )
            {
                indexArticlesModel.CurrentUser = GetUserForIndex();

                indexArticlesModel.RecommendedArticle = GetRecommendedArticlesForIndex();
                indexArticlesModel.NewestArticleUserPairList = GetNewestArticlesForIndex();
                indexArticlesModel.MostViewedUserArticlePairList = GetMostViewedArticlesForIndex();

                CutContentString(indexArticlesModel.RecommendedArticle, 100);
                CutSubjectString(indexArticlesModel.RecommendedArticle, 100);

                CutContentString(indexArticlesModel.NewestArticleUserPairList, 100);
                CutSubjectString(indexArticlesModel.NewestArticleUserPairList, 100);

                CutContentString(indexArticlesModel.NewestArticleUserPairList, 100);
                CutSubjectString(indexArticlesModel.NewestArticleUserPairList, 100);

                if ( User.Identity.IsAuthenticated )
                {
                    indexArticlesModel.MyArticleList = GetMyArticleArticlesForIndex(indexArticlesModel.CurrentUser);
                    CutContentString(indexArticlesModel.MyArticleList, 100);
                    CutSubjectString(indexArticlesModel.MyArticleList, 100);
                }
                else
                {
                    indexArticlesModel.MyArticleList = new List<Article>();
                }
            }
        }

        private void CutSubjectString(List<ArticleUserPair> newestArticleUserPairList, int iLength)
        {
            int iTempLength = -1;
            int iPreviousLength = 0;

            for (int iCounter = 0; iCounter < newestArticleUserPairList.Count; iCounter++)
            {
                iTempLength = (newestArticleUserPairList[iCounter].Article.Subject.Length <= iLength)
                    ? newestArticleUserPairList[iCounter].Article.Subject.Length : iLength;

                iPreviousLength = newestArticleUserPairList[iCounter].Article.Subject.Length;
                newestArticleUserPairList[iCounter].Article.Subject
                    = newestArticleUserPairList[iCounter].Article.Subject.Substring(0, iTempLength);

                if (iPreviousLength > iLength)
                {
                    newestArticleUserPairList[iCounter].Article.Subject += "...";
                }
            }
        }

        private void CutContentString(List<ArticleUserPair> newestArticleUserPairList, int iLength)
        {
            int iTempLength = -1;
            int iPreviousLength = 0;

            for (int iCounter = 0; iCounter < newestArticleUserPairList.Count; iCounter++)
            {
                iTempLength = (newestArticleUserPairList[iCounter].Article.Content.Length <= iLength)
                    ? newestArticleUserPairList[iCounter].Article.Content.Length : iLength;

                iPreviousLength = newestArticleUserPairList[iCounter].Article.Content.Length;
                newestArticleUserPairList[iCounter].Article.Content
                    = newestArticleUserPairList[iCounter].Article.Content.Substring(0, iTempLength);

                if (iPreviousLength > iLength)
                {
                    newestArticleUserPairList[iCounter].Article.Content += "...";
                }
            }
        }

        private void CutSubjectString(List<Article> myArticleList, int iLength )
        {
            int iTempLength = -1;
            int iPreviousLength = 0;

            for ( int iCounter = 0; iCounter < myArticleList.Count; iCounter++ )
            {
                iTempLength = (myArticleList[iCounter].Subject.Length <= iLength)
                    ? myArticleList[iCounter].Subject.Length : iLength;

                iPreviousLength = myArticleList[iCounter].Subject.Length;
                myArticleList[iCounter].Content = myArticleList[iCounter].Subject.Substring( 0, iTempLength);

                if (iPreviousLength > iLength)
                {
                    myArticleList[iCounter].Subject += "...";
                }
            }
        }

        private void CutContentString( List<Article> myArticleList, int iLength )
        {
            int iTempLength = -1;
            int iPreviousLength = 0;

            for (int iCounter = 0; iCounter < myArticleList.Count; iCounter++)
            {
                iTempLength = (myArticleList[iCounter].Content.Length <= iLength)
                    ? myArticleList[iCounter].Content.Length : iLength;

                iPreviousLength = myArticleList[iCounter].Content.Length;
                myArticleList[iCounter].Content = myArticleList[iCounter].Content.Substring(0, iTempLength);

                if(iPreviousLength > iLength )
                {
                    myArticleList[iCounter].Content += "...";
                }
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

        private List<ArticleUserPair> GetRecommendedArticlesForIndex()
        {
            const int iNumberOfArticlesResult = 3;
            HashSet<int> recommendedArticleIndexesHashSet = new HashSet<int>();
            List<Article> articlesFromDb = _repositoryWrapper.Articles.FindAll().ToList<Article>();
            List<Article> recommendedArticles = new List<Article>();
            List<ArticleUserPair> recommendedUserArticles = null;

            int iArticleCounter = 0;
            int iArticlesFromDbCount = articlesFromDb.Count;
            do
            {
                if( recommendedArticleIndexesHashSet.Add( new Random().Next(articlesFromDb.Count ) ) )
                {
                    iArticleCounter++;
                }

                if( iArticlesFromDbCount == iArticleCounter )
                {
                    break;
                }
            }
            while( iArticleCounter < iNumberOfArticlesResult);
      

            foreach( int iHashSetElementValue in recommendedArticleIndexesHashSet )
            {
                recommendedArticles.Add( articlesFromDb[iHashSetElementValue] );
            }

            recommendedUserArticles = UserTools.GetUserArticlePair(_repositoryWrapper, recommendedArticles);

            return recommendedUserArticles;
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

            return currentUser;
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
