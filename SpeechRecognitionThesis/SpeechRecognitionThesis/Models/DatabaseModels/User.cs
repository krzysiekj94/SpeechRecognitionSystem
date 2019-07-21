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
        [StringLength(20)]
        public string NickName { get; set; }

        [StringLength(512)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        public string CreateAccountDate { get; set; }

        [DataType(DataType.DateTime)]

        public string LastUpdateAccountDate { get; set; }

        public AccountActiveState ActiveAccountState { get; set; }
    }
}
