﻿using SpeechRecognitionThesis.Models.Database;
using SpeechRecognitionThesis.Models.Repository.Interfaces;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext   _repositoryContext;
        private RepositoryManager   _repositoryManager;

        public RepositoryWrapper(RepositoryContext repositoryContext )
        {
            _repositoryContext = repositoryContext;
            _repositoryManager = new RepositoryManager(_repositoryContext);
        }

        public IAccountRepository Account
        {
            get
            {
                return _repositoryManager.Account;
            }
        }

        public IArticleRepository Articles
        {
            get
            {
                return _repositoryManager.Articles;
            }
        }

        public IUserArticlesRepository UserArticles
        {
            get
            {
                return _repositoryManager.UserArticles;
            }
        }

        public IArticleCategoryRepository ArticlesCategory
        {
            get
            {
                return _repositoryManager.ArticlesCategory;
            }
        }

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}