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
        private IUserArticlesRepository _userArticlesRepository;

        public UserArticlesRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _userArticlesRepository = new UserArticlesRepository(repositoryContext);
        }

        public List<UserArticles> GetUserArticles()
        {
            return _userArticlesRepository.FindAll().ToList();
        }

        public IEnumerable<UserArticles> GetUserArticles(long lUserId)
        {
            return _userArticlesRepository.FindAll()
                .Where(user => user.UserRefId == lUserId).ToList();
        }
    }
}
