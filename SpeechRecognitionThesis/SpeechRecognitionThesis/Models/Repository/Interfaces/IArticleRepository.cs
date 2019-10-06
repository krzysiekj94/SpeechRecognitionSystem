using SpeechRecognitionThesis.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public interface IArticleRepository : IDataRepository<Article>
    {
        Article GetLastAddedArticle();
        void DeleteArticles(IEnumerable<UserArticles> userArticlesEnumerable);
        List<Article> GetArticles(IEnumerable<UserArticles> userArticlesEnumerable);
        Article GetArticle( long lArticleId );
        List<Article> GetNewestArticles( int iAmountArticles );
        List<Article> GetArticlesFromCategory( long lCategoryId );
        List<Article> GetMostViewedArticles( int iAmountArticles );
    }
}
