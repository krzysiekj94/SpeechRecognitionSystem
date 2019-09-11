using SpeechRecognitionThesis.Models.DatabaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeechRecognitionThesis.Models
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime InsertionDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool AvailabilityStatus { get; set; }

        [ForeignKey("ArticleCategory")]
        public long ArticleCategoryRefId { get; set; }
        public ArticleCategory ArticleCategory { get; set; }
    }
}
