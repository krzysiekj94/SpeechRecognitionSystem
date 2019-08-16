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
        public long AuthorId { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public DateTime InsertionDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
