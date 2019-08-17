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
        private IUserArticlesRepository _userArticlesRepository;

        public ArticleRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _userArticlesRepository = new UserArticlesRepository( repositoryContext );
        }

        public Article GetLastAddedArticle()
        {
            return FindAll()
                .OrderByDescending( article => article.Id )
                .FirstOrDefault();
        }

        public List<UserArticles> GetUserArticles()
        {
            return _userArticlesRepository.FindAll().ToList();
        }
    }
}
