using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.DatabaseModels
{
    public class UserArticles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("User")]
        public long UserRefId { get; set; }
        public User User { get; set; }

        [ForeignKey("Article")]
        public long ArticleRefId { get; set; }
        public Article Article { get; set; }

        [DataType(DataType.DateTime)]
        public string ArticleModificationDate { get; set; }
    }
}
