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

        [ForeignKey("ArticleCategory")]
        public long ArticleCategoryRefId { get; set; }
        public ArticleCategory ArticleCategory { get; set; }

        [StringLength(200)]
        public string Subject { get; set; }

        [StringLength(4000)]
        public string Content { get; set; }

        [StringLength(50)]
        [DataType(DataType.DateTime)]
        public string ArticleModificationDate { get; set; }
        public long NumberOfViews { get; set; }
    }
}
