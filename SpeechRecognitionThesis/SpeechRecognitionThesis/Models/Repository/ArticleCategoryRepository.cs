using SpeechRecognitionThesis.Models.Database;
using SpeechRecognitionThesis.Models.DatabaseModels;
using SpeechRecognitionThesis.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<ArticleCategory>, IArticleCategoryRepository
    {
        RepositoryContext _repositoryContext;

        public ArticleCategoryRepository( RepositoryContext repositoryContext )
            : base( repositoryContext )
        {
            _repositoryContext = repositoryContext;
        }

        public ArticleCategory GetCategory( long lArticleCategoryId )
        {
            return FindAll()
                   .FirstOrDefault( category => category.Id == lArticleCategoryId );
        }

        public string GetCategoryName( long lArticleCategoryId )
        {
            ArticleCategory articleCategory = GetCategory( lArticleCategoryId );

            return ( articleCategory != null ) ? articleCategory.Name : string.Empty;
        }
    }
}