using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeechRecognitionThesis.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        [Required]
        public string NickName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreateAccountDate { get; set; }
        public DateTime LastUpdateAccountDate { get; set; }
        public bool IsActiveAccount { get; set; }
    }
}
