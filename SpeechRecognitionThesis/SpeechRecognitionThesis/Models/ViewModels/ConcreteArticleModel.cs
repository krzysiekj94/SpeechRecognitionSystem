using SpeechRecognitionThesis.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.ViewModels
{
    public class ConcreteArticleModel
    {
        public UserArticles UserArticle { get; set; }
        public Article Article { get; set; }
    }
}
