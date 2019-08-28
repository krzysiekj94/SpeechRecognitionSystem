using SpeechRecognitionThesis.Models;

namespace SpeechRecognitionThesis.Models.ViewModels
{
    public class AccountUserModel
    {
        public int iAmountOfArticles;
        public string ConfirmPassword { get; set; }

        public User User { get; set; }
    }
}