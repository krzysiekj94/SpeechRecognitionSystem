using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Scripts
{
    public class ScriptManager
    {
        const string PAGE_RELATIVE_PATH_STRING      = "/js/speech_engine.js";
        const string NEWEST_RELATIVE_PATH_STRING    = "/articles/newest";

        public static string getArtyomScriptPathString( string urlRelativePathString )
        {
            string urlPathString = PAGE_RELATIVE_PATH_STRING;

            switch( urlRelativePathString )
            {
                case NEWEST_RELATIVE_PATH_STRING:
                    urlPathString = NEWEST_RELATIVE_PATH_STRING;
                    break;
                default:
                    break;
            }

            return urlPathString;
        }
    }
}
