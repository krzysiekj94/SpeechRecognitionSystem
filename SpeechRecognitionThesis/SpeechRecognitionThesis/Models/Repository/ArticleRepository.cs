﻿using SpeechRecognitionThesis.Models.Database;
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

        public void DeleteArticles(IEnumerable<UserArticles> userArticlesEnumerable)
        {
            List<Article> articleEnumerable = new List<Article>();
            userArticlesEnumerable.ToList().ForEach(element => articleEnumerable.Add(new Article { Id = element.ArticleRefId } ) );
            Delete(articleEnumerable);
        }

        public List<Article> GetArticles(IEnumerable<UserArticles> userArticlesEnumerable)
        {
            List<Article> userArticleList = new List<Article>();
            userArticlesEnumerable.ToList().ForEach( 
                element => userArticleList.Add( FindAll()
                                                .FirstOrDefault( article => article.Id == element.ArticleRefId ) ) );

            return userArticleList;
        }

        public Article GetLastAddedArticle()
        {
            return FindAll()
                   .OrderByDescending( article => article.Id )
                   .FirstOrDefault();
        }

        public List<Article> GetUserArticles()
        {
            return FindAll()
                   .ToList();
        }

        public Article GetArticle( long lArticleId )
        {
            return FindAll()
                   .FirstOrDefault( article => article.Id == lArticleId );
        }

        public List<Article> GetNewestArticles( int iAmountArticles )
        {
            return FindAll()
                   .OrderByDescending(article => article.Id)
                   .Take( iAmountArticles )
                   .ToList();
        }

        public List<Article> GetArticlesFromCategory( long lCategoryId )
        {
            return FindAll()
                    .Where( article => article.ArticleCategoryRefId == lCategoryId ).ToList();
        }

        public List<Article> GetMostViewedArticles(int iAmountArticles)
        {
            return FindAll()
                    .OrderByDescending( article => article.NumberOfViews )
                    .Take( iAmountArticles )
                    .ToList();
        }
    }
}