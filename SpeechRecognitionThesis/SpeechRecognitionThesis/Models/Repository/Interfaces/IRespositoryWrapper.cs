namespace SpeechRecognitionThesis.Models.Repository
{
    public interface IRespositoryWrapper
    {
        IAccountRepository Account  { get; }
        IArticleRepository Articles { get; }

        void Save();
    }
}
