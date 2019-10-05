using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.ViewModels
{
    public class ArticlesFromCategoryModel
    {
        public List<ArticleUserPair> ArticleUser { get; set; }
        public string CategoryName { get; set; }
    }
}
