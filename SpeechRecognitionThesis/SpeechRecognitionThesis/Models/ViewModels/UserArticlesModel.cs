using SpeechRecognitionThesis.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.ViewModels
{
    public class UserArticlesModel
    {
        public User User { get; set; }
        public List<Article> UserArticlesList { get; set; }
    }
}
