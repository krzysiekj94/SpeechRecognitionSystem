using SpeechRecognitionThesis.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository.Interfaces
{
    public interface IUserArticlesRepository : IDataRepository<UserArticles>
    {
        IEnumerable<UserArticles> GetUserArticles(long lUserId);
    }
}
