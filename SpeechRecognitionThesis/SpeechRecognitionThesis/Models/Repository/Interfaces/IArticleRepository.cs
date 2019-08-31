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
    }
}
