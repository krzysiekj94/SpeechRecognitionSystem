﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Scripts
{
    public class ScriptManager
    {
        const string SPEECH_MAIN_PAGE_RELATIVE_PATH_STRING              = "/js/speech_engine.js";
        const string SPEECH_ARTICLES_PAGE_RELATIVE_PATH_STRING          = "/js/articles_speech.js";
        const string ADD_ARTICLES_RELATIVE_PATH_STRING                  = "/articles/add";
        const string REGISTER_NEW_USER_SCRIPT_RELATIVE_PATH_STRING      = "/js/register.js";
        const string REGISTER_NEW_USER_RELATIVE_PATH_STRING             = "/register";

        public static string getArtyomScriptPathString( string urlRelativePathString )
        {
            string urlPathString = SPEECH_MAIN_PAGE_RELATIVE_PATH_STRING;

            switch( urlRelativePathString.ToLower() )
            {
                case ADD_ARTICLES_RELATIVE_PATH_STRING:
                    urlPathString = SPEECH_ARTICLES_PAGE_RELATIVE_PATH_STRING;
                    break;
                default:
                    break;
            }

            return urlPathString;
        }

        public static string getRegisterScriptPathString( string urlRelativePathString )
        {
            string registerScriptPathString = string.Empty;

            switch( urlRelativePathString.ToLower() )
            {
                case REGISTER_NEW_USER_RELATIVE_PATH_STRING:
                    registerScriptPathString = REGISTER_NEW_USER_SCRIPT_RELATIVE_PATH_STRING;
                    break;
                default:
                    break;
            }

            return registerScriptPathString;
        }
    }
}
