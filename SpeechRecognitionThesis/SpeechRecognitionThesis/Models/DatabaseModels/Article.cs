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

        [ForeignKey("ArticleCategory")]
        public long ArticleCategoryRefId { get; set; }
        public ArticleCategory ArticleCategory { get; set; }

        [DataType(DataType.DateTime)]
        public string ArticleModificationDate { get; set; }
        public long NumberOfViews { get; set; }
    }
}
