using Microsoft.EntityFrameworkCore;
using SpeechRecognitionThesis.Models.DatabaseModels;
using SpeechRecognitionThesis.Models.Scripts;
using System;

namespace SpeechRecognitionThesis.Models.Database
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions articleDbContextOptions)
            : base(articleDbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitArticles(modelBuilder);
            InitUsers(modelBuilder);
            InitUserArticles(modelBuilder);
        }

        private void InitUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                NickName = "SuperBass",
                Password = UserTools.ConvertInputTextToSha512("SuperPasswor123"),
                Email = "bas@gmail.com",
                CreateAccountDate = new DateTime(2019, 05, 30).ToString(),
                LastUpdateAccountDate = new DateTime(2019, 06, 20).ToString(),
                ActiveAccountState = AccountActiveState.Active,
                IsLogged = false
            },
           new User
           {
               UserId = 2,
               NickName = " RobertSon",
               Password = UserTools.ConvertInputTextToSha512("passwordTest231"),
               Email = "robert@mail.com",
               CreateAccountDate = new DateTime(2019, 05, 21).ToString(),
               LastUpdateAccountDate = new DateTime(2019, 06, 23).ToString(),
               ActiveAccountState = AccountActiveState.Active,
               IsLogged = false
           });
        }

        private void InitArticles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 1,
                AuthorId = 1,
                Content = "To jest artykuł 1",
                AuthorName = "Krystian B.",
                InsertionDate = new DateTime(2017, 04, 25),
                LastUpdateDate = new DateTime(2018, 05, 11),

            },
            new Article
            {
                ArticleId = 2,
                AuthorId = 1,
                Content = "To jest artykuł 2",
                AuthorName = "Roman Z.",
                InsertionDate = new DateTime(2019, 05, 05),
                LastUpdateDate = new DateTime(2019, 05, 05),
            });
        }

        private void InitUserArticles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserArticles>().HasData(new UserArticles
            {
                UserRefId = 1,
                ArticleRefId = 1,
                AddArticleToUserDate = DateTime.Now.ToString()
            },
            new UserArticles
            {
                UserRefId = 1,
                ArticleRefId = 2,
                AddArticleToUserDate = DateTime.Now.ToString()
            },
            new UserArticles
            {
                UserRefId = 2,
                ArticleRefId = 1,
                AddArticleToUserDate = DateTime.Now.ToString()
            });
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserArticles> UserArticles { get; set; }
    }
}