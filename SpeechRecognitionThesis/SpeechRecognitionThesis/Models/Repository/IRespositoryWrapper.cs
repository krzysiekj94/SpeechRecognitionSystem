using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    interface IRespositoryWrapper
    {
        IAccountRepository Account { get; }
        void Save();
    }
}
