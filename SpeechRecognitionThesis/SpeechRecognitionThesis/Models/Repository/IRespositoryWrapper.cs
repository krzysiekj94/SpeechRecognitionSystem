﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public interface IRespositoryWrapper
    {
        IAccountRepository Account  { get; }
        IArticleRepository Articles { get; }
        void Save();
    }
}
