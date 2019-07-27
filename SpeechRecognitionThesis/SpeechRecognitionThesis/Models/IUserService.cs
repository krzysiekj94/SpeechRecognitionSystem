using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models
{
    interface IUserService
    {
        Task<User> Authenticate(string usernameString, string passwordString);
        Task<IEnumerable<User>> GetAll();
    }
}
