using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Scripts
{
    public class ScriptManager
    {
        const string SPEECH_MAIN_PAGE_RELATIVE_PATH_STRING          = "/js/speech_engine.js";
        const string SPEECH_ARTICLES_PAGE_RELATIVE_PATH_STRING      = "/js/articles_speech.js";
        const string NEWEST_ARTICLES_RELATIVE_PATH_STRING           = "/articles/newest";

        public static string getArtyomScriptPathString( string urlRelativePathString )
        {
            string urlPathString = SPEECH_MAIN_PAGE_RELATIVE_PATH_STRING;

            switch( urlRelativePathString )
            {
                case NEWEST_ARTICLES_RELATIVE_PATH_STRING:
                    urlPathString = SPEECH_ARTICLES_PAGE_RELATIVE_PATH_STRING;
                    break;
                default:
                    break;
            }

            return urlPathString;
        }
    }
}
