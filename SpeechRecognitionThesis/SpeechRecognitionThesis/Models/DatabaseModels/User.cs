using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeechRecognitionThesis.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(20)]
        public string NickName { get; set; }

        [StringLength(512)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        [StringLength(50)]
        public string CreateAccountDate { get; set; }

        [StringLength(50)]
        [DataType(DataType.DateTime)]
        public string LastUpdateAccountDate { get; set; }

        [StringLength(50)]
        [DataType(DataType.DateTime)]
        public string LastLoggedAccountDate { get; set; }

        public int AvatarId { get; set; }
    }
}
