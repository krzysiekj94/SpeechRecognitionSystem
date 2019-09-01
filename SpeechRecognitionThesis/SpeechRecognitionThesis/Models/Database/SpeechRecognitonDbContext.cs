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
                Id = 1,
                NickName = UserTools.ANONYMOUS_USER_NICKNAME,
                Password = UserTools.ConvertInputTextToSha512( UserTools.ANONYMOUS_USER_NICKNAME ),
                Email = "guest@speechrecognition.com",
                CreateAccountDate = new DateTime(2019, 05, 30).ToString(),
                LastUpdateAccountDate = new DateTime(2019, 06, 20).ToString(),
                LastLoggedAccountDate = new DateTime(2019, 08, 24).ToString(),
                ActiveAccountState = AccountActiveState.Active,
                IsLogged = true
            },
           new User
           {
               Id = 2,
               NickName = "RobertSon",
               Password = UserTools.ConvertInputTextToSha512("passwordTest231"),
               Email = "robert@mail.com",
               CreateAccountDate = new DateTime(2019, 05, 21).ToString(),
               LastUpdateAccountDate = new DateTime(2019, 06, 23).ToString(),
               LastLoggedAccountDate = new DateTime(2019, 08, 23).ToString(),
               ActiveAccountState = AccountActiveState.Active,
               IsLogged = false
           });
        }

        private void InitArticles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasData(new Article
            {
                Id = 1,
                Content = "To jest artykuł 1",
                InsertionDate = new DateTime(2017, 04, 25),
                LastUpdateDate = new DateTime(2018, 05, 11),

            },
            new Article
            {
                Id = 2,
                Content = "To jest artykuł 2",
                InsertionDate = new DateTime(2019, 05, 05),
                LastUpdateDate = new DateTime(2019, 05, 05),
            });
        }

        private void InitUserArticles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserArticles>().HasData(new UserArticles
            {
                Id = 1,
                UserRefId = 1,
                ArticleRefId = 1,
                ArticleModificationDate = DateTime.Now.ToString()
            },
            new UserArticles
            {
                Id = 2,
                UserRefId = 1,
                ArticleRefId = 2,
                ArticleModificationDate = DateTime.Now.ToString()
            },
            new UserArticles
            {
                Id = 3,
                UserRefId = 2,
                ArticleRefId = 1,
                ArticleModificationDate = DateTime.Now.ToString(),
            });
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserArticles> UserArticles { get; set; }
    }
}