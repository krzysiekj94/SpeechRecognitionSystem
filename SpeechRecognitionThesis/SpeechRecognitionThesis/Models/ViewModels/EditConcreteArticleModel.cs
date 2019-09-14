using SpeechRecognitionThesis.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.ViewModels
{
    public class EditConcreteArticleModel
    {
        public ConcreteArticleModel ConcreteArticleModel { get; set; }
        public List<ArticleCategory> ArticleCategoryList { get; set; }
    }
}
