using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.ViewModels
{
    public class IndexArticlesModel
    {
        public List<ArticleUserPair> NewestArticleUserPairList { get; set; }
        public List<ArticleUserPair> MostViewedUserArticlePairList { get; set; }
        public List<Article> MyArticleList { get; set; }
        public List<ArticleUserPair> RecommendedArticle { get; set; }
        public User CurrentUser { get; set; }
    }
}
