using SpeechRecognitionThesis.Models.Database;
using SpeechRecognitionThesis.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.DataManager
{
    public class ArticlesDbManager //: IDataRepository<Article>
    {
        readonly RepositoryContext _speechRecognitionDbContext;

        public ArticlesDbManager(RepositoryContext context)
        {
            _speechRecognitionDbContext = context;
        }

        public void Add(Article entity)
        {
            _speechRecognitionDbContext.Articles.Add(entity);
            _speechRecognitionDbContext.SaveChanges();
        }

        public void Delete(Article article)
        {
            _speechRecognitionDbContext.Articles.Remove(article);
            _speechRecognitionDbContext.SaveChanges();
        }

        public Article Get(long id)
        {
            return _speechRecognitionDbContext.Articles
                  .FirstOrDefault(e => e.ArticleId == id);
        }

        public IEnumerable<Article> GetAll()
        {
            return _speechRecognitionDbContext.Articles.ToList();
        }

        public void Update(Article article, Article entity)
        {
            article.AuthorId = entity.AuthorId;
            article.AuthorName = entity.AuthorName;
            article.Content = entity.Content;
            article.InsertionDate = entity.InsertionDate;
            article.LastUpdateDate = entity.LastUpdateDate;

            _speechRecognitionDbContext.SaveChanges();
        }
    }
}
