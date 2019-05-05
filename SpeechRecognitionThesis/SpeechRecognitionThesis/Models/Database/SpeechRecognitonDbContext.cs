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

        public DbSet<Article> Articles { get; set; }
    }
}
