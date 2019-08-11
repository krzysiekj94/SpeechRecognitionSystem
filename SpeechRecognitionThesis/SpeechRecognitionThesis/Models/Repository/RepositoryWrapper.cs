﻿using SpeechRecognitionThesis.Models.Database;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class RepositoryWrapper : IRespositoryWrapper
    {
        private RepositoryContext       _repositoryContext;
        private IAccountRepository      _accountRepository;
        private IArticleRepository      _articleRepository;

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

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}