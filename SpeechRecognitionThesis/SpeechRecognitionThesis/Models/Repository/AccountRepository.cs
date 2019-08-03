using SpeechRecognitionThesis.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class AccountRepository : RepositoryBase<User>, IAccountRepository
    {
        RepositoryContext _repositoryContext;

        public AccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public User Authenticate(string username, string password)
        {
            User user = _repositoryContext.Users.SingleOrDefault(x => x.NickName == username && x.Password == password);
            //#TODO


            return user;
        }
    }
}
