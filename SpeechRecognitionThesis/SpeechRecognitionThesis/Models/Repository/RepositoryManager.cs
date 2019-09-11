using SpeechRecognitionThesis.Models.Database;
using SpeechRecognitionThesis.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class RepositoryManager
    {
        private IAccountRepository          _accountRepository;
        private IArticleRepository          _articleRepository;
        private IArticleCategoryRepository  _articleCategoryRepository;
        private IUserArticlesRepository     _userArticlesRepository;
        private RepositoryContext           _repositoryContext;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IAccountRepository Account
        {
            get
            {
                if (_accountRepository == null)
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
                if (_articleRepository == null)
                {
                    _articleRepository = new ArticleRepository(_repositoryContext);
                }

                return _articleRepository;
            }
        }

        public IArticleCategoryRepository ArticlesCategory
        {
            get
            {
                if (_articleCategoryRepository == null)
                {
                    _articleCategoryRepository = new ArticleCategoryRepository(_repositoryContext);
                }

                return _articleCategoryRepository;
            }
        }

        public IUserArticlesRepository UserArticles
        {
            get
            {
                if (_userArticlesRepository == null)
                {
                    _userArticlesRepository = new UserArticlesRepository(_repositoryContext);
                }

                return _userArticlesRepository;
            }
        }
    }
}
