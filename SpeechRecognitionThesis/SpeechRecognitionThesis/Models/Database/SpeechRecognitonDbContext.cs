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
            InitArticlesCategory(modelBuilder);
            InitArticles(modelBuilder);
            InitUsers(modelBuilder);
            InitUserArticles(modelBuilder);
        }

        private void InitArticlesCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleCategory>().HasData(new ArticleCategory
            {
                Id = 1,
                Name = "Sport"
            },
            new ArticleCategory
            {
                Id = 2,
                Name = "Nauka"
            },
            new ArticleCategory
            {
                Id = 3,
                Name = "Świat"
            },
            new ArticleCategory
            {
                Id = 4,
                Name = "Kraj"
            },
            new ArticleCategory
            {
                Id = 5,
                Name = "Popularnonaukowe"
            });
        }

        private void InitUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                NickName = UserTools.ANONYMOUS_USER_NICKNAME,
                Password = UserTools.ConvertInputTextToSha512(UserTools.ANONYMOUS_USER_NICKNAME),
                Email = "guest@speechrecognition.com",
                CreateAccountDate = new DateTime(2019, 05, 30).ToString(),
                LastUpdateAccountDate = new DateTime(2019, 06, 20).ToString(),
                LastLoggedAccountDate = new DateTime(2019, 08, 24).ToString(),
                AvatarId = 1
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
               AvatarId = 2
           });
        }

        private void InitArticles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasData(new Article
            {
                Id = 1,
                ArticleCategoryRefId = 1,
                Subject = "Artykuł 1",
                Content = "To jest treść artykułu 1",
                ArticleModificationDate = DateTime.Now.ToString(),
                NumberOfViews = 10,
            },
            new Article
            {
                Id = 2,
                ArticleCategoryRefId = 4,
                Subject = "Artykuł 2",
                Content = "To jest artykuł 2",
                ArticleModificationDate = DateTime.Now.ToString(),
                NumberOfViews = 20,
            });
        }

        private void InitUserArticles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserArticles>().HasData(new UserArticles
            {
                Id = 1,
                UserRefId = 1,
                ArticleRefId = 1,
            },
            new UserArticles
            {
                Id = 2,
                UserRefId = 1,
                ArticleRefId = 2,
            },
            new UserArticles
            {
                Id = 3,
                UserRefId = 2,
                ArticleRefId = 1,
            });
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserArticles> UserArticles { get; set; }
    }
}