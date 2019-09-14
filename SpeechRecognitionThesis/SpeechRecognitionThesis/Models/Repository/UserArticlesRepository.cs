using SpeechRecognitionThesis.Models.Database;
using SpeechRecognitionThesis.Models.DatabaseModels;
using SpeechRecognitionThesis.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class UserArticlesRepository : RepositoryBase<UserArticles>, IUserArticlesRepository
    {
        private RepositoryContext _userArticlesRepository;

        public UserArticlesRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _userArticlesRepository = repositoryContext;
        }

        public IEnumerable<UserArticles> DeleteArticles( long lUserId )
        {
            IEnumerable<UserArticles> userArticlesEnumerable = GetUserArticles(lUserId);

            Delete(userArticlesEnumerable);

            return userArticlesEnumerable;
        }

        public List<UserArticles> GetUserArticles()
        {
            return FindAll().ToList();
        }

        public IEnumerable<UserArticles> GetUserArticles(long lUserId)
        {
            return FindAll()
                .Where(user => user.UserRefId == lUserId).ToList();
        }

        public UserArticles GetUserArticle(long lArticleId, long lUserId)
        {
            return FindAll().FirstOrDefault( userArticle => 
                userArticle.ArticleRefId == lArticleId && userArticle.UserRefId == lUserId );
        }
    }
}
