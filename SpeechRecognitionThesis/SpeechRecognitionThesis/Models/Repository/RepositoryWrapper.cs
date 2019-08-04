using SpeechRecognitionThesis.Models.Database;
using SpeechRecognitionThesis.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class RepositoryWrapper : IRespositoryWrapper
    {
        private RepositoryContext       _repositoryContext;
        private IAccountRepository      _accountRepository;
        private IArticleRepository      _articleRepository;
        private IUserSessionRepository  _userSessionRepository;

        public RepositoryWrapper(RepositoryContext repositoryContext )
        {
            _repositoryContext = repositoryContext;
        }

        public IAccountRepository Account
        {
            get
            {
                if( _accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_repositoryContext);
                }

                return _accountRepository;
            }
        }

        public IArticleRepository Articles
        {
            get
            {
                if( _articleRepository == null)
                {
                    _articleRepository = new ArticleRepository(_repositoryContext);
                }

                return _articleRepository;
            }
        }

        public IUserSessionRepository UserSessions
        {
            get
            {
                if( _userSessionRepository == null )
                {
                    _userSessionRepository = new UserSessionRepository(_repositoryContext);
                }

                return _userSessionRepository;
            }
        }


        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}