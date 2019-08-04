using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.DatabaseModels
{
    public class UserSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("UserId")]
        public long UserId { get; set; }

        public string SessionData { get; set; }
    }
}
