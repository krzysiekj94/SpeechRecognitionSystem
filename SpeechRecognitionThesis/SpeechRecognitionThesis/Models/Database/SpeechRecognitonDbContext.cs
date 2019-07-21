using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Database
{
    public class SpeechRecognitonDbContext : DbContext
    {
        public SpeechRecognitonDbContext( DbContextOptions articleDbContextOptions )
            : base(articleDbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitArticles(modelBuilder);
            InitUsers(modelBuilder);
        }

        private void InitUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                NickName = "SuperBass",
                Password = "0x07e547d9586f6a73f73fbac0435ed76951218fb7d0c8d788a309d785436bbb642e93a252a954f23912547d1e8a3b5ed6e1bfd7097821233fa0538f3db854fee6",
                Email="bas@gmail.com",
                CreateAccountDate = new DateTime(2019, 05, 30).ToString(),
                LastUpdateAccountDate = new DateTime(2019, 06, 20).ToString(),
                ActiveAccountState = AccountActiveState.Active
            },
           new User
           {
               UserId = 2,
               NickName = " RobertSon",
               Password = "0x07e547d9586f6a73f73fbac0435ed76951218fb7d0c8d788a309d785436bbb642e93a252a954f23912547d1e8a3b5ed6e1bfd7097821233fa0538f3db854fee6",
               Email="robert@mail.com",
               CreateAccountDate = new DateTime(2019, 05, 21).ToString(),
               LastUpdateAccountDate = new DateTime(2019, 06, 23).ToString(),
               ActiveAccountState = AccountActiveState.Active
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


        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
