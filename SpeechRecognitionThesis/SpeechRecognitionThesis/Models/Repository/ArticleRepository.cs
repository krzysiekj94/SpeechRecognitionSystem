using SpeechRecognitionThesis.Models.Database;
using SpeechRecognitionThesis.Models.DatabaseModels;
using SpeechRecognitionThesis.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        RepositoryContext _repositoryContext;

        public ArticleRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public Article GetLastAddedArticle()
        {
            return FindAll()
                .OrderByDescending( article => article.Id )
                .FirstOrDefault();
        }

        public List<Article> GetUserArticles()
        {
            return FindAll().ToList();
        }
    }
}