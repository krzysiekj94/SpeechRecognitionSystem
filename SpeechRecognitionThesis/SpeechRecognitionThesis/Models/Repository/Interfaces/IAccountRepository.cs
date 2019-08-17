using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public interface IAccountRepository : IDataRepository<User>
    {
        Task<User> Authenticate( string usernameString, string passwordString );
        User GetUser( long lUserId );
        User GetAnonymousUser();
    }
}
