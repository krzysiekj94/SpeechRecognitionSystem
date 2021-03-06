﻿using SpeechRecognitionThesis.Models.Repository.Interfaces;

namespace SpeechRecognitionThesis.Models.Repository
{
    public interface IRepositoryWrapper
    {
        IAccountRepository Account  { get; }
        IArticleRepository Articles { get; }
        IUserArticlesRepository UserArticles { get; }
        IArticleCategoryRepository ArticlesCategory { get; }

        void Save();
    }
}
