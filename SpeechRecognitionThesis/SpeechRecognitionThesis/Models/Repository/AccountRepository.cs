using SpeechRecognitionThesis.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class AccountRepository : RepositoryBase<User>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
