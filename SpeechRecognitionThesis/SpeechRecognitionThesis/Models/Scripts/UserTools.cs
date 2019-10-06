using SpeechRecognitionThesis.Models.DatabaseModels;
using SpeechRecognitionThesis.Models.Repository;
using SpeechRecognitionThesis.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Scripts
{
    public class UserTools
    {
        public static readonly string   ANONYMOUS_USER_NICKNAME = "Guest";
        public static readonly string   REGISTER_DATE_PROPERTY_STRING = "RegisterDateString";
        public static readonly string   USER_LAST_LOGGED_DATE_PROPERTY_STRING = "LastLoggedDateString";
        public static readonly string   USER_ID_PROPERTY_STRING = "UserId";
        public static readonly string   USER_EMAIL_PROPERTY_STRING = "Email";
        public static readonly string   URL_WEBSITE_STRING = "http://localhost:8080/";

        static public bool ConvertPasswordToSha512(User registerUser)
        {
            bool bConvertPasswordResult = false;
            string hashPasswordString = string.Empty;
            string passwordString = registerUser.Password;

            if (passwordString.Length > 0)
            {
                hashPasswordString = Sha512Manager.ComputeHashString(registerUser.Password);

                if (hashPasswordString.Length > 0)
                {
                    registerUser.Password = hashPasswordString;
                    bConvertPasswordResult = true;
                }
            }

            return bConvertPasswordResult;
        }

        static public string ConvertInputTextToSha512(string inputTextString )
        {
            return Sha512Manager.ComputeHashString(inputTextString);
        }

        static bool AuthenticateUserProcess(User loginUser)
        {
            return (loginUser != null)
                    && UserTools.ConvertPasswordToSha512(loginUser);
        }

        static public List<ArticleUserPair> GetUserArticlePair( IRespositoryWrapper repositoryWrapper, 
                                                                List<Article> articleList )
        {
            if( repositoryWrapper == null || articleList == null )
            {
                return null;
            }

            List<ArticleUserPair> articleUserPairList = new List<ArticleUserPair>();
            UserArticles userArticle = null;
            User tempUser = null;

            foreach( var article in articleList )
            {
                article.ArticleCategory = repositoryWrapper.ArticlesCategory.GetCategory(article.ArticleCategoryRefId);
                userArticle = repositoryWrapper.UserArticles.GetUserArticle(article);

                if( userArticle != null )
                {
                    tempUser = repositoryWrapper.Account.GetUser(userArticle.UserRefId);

                    if( tempUser != null )
                    {
                        articleUserPairList.Add(new ArticleUserPair()
                        {
                            Article = article,
                            User = tempUser
                        });
                    }
                }
            }

            return articleUserPairList;
        }

        static public string GetUserImagePath( int iAvatarId )
        {
            return "/images/" + (iAvatarId + 1).ToString() + ".png";
        }

        static public string GetCategoryImagePath( long iCategoryId )
        {
            string categoryImagePathString = "/images/category/";

            switch( (CategoryId)iCategoryId )
            {
                case CategoryId.COUNTRY_CATEGORY_ID:
                    categoryImagePathString += "poland.jpg";
                    break;
                case CategoryId.POPULAR_SCIENCE_CATEGORY_ID:
                    categoryImagePathString += "popular_science.jpg";
                    break;
                case CategoryId.SCIENCE_CATEGORY_ID:
                    categoryImagePathString += "science.jpg";
                    break;
                case CategoryId.SPORT_CATEGORY_ID:
                    categoryImagePathString += "ball.jpg";
                    break;
                case CategoryId.WORLD_CATEGORY_ID:
                    categoryImagePathString += "world.jpg";
                    break;
                case CategoryId.NONE:
                    categoryImagePathString += string.Empty;
                    break;
                default:
                    categoryImagePathString += string.Empty;
                    break;
            }

            return categoryImagePathString;
        }
    }
}
