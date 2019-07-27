using Microsoft.AspNetCore.Authentication;

namespace SpeechRecognitionThesis
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Realm { get; set; }
    }
}