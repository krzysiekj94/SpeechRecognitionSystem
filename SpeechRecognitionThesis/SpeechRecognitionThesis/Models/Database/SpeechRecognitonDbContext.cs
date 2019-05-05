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
                LastUpdateDate = new DateTime(2019,05,05),
            });
        }

        public DbSet<Article> Articles { get; set; }
    }
}
