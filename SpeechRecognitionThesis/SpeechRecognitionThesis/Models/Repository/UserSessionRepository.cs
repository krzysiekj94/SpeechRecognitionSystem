using SpeechRecognitionThesis.Models.Database;
using SpeechRecognitionThesis.Models.DatabaseModels;
using SpeechRecognitionThesis.Models.Repository.Interfaces;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class UserSessionRepository : RepositoryBase<UserSession>, IUserSessionRepository
    {
        public UserSessionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
