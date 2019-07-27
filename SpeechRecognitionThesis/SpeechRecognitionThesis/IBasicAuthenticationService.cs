using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis
{
    public interface IBasicAuthenticationService
    {
        Task<bool> IsValidUserAsync(string user, string password);
    }
}
