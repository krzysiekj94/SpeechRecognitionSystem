using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Scripts
{
    public class ScriptManager
    {
        const string SPEECH_MAIN_PAGE_RELATIVE_PATH_STRING              = "/js/speech_engine.js";
        const string SPEECH_ARTICLES_PAGE_RELATIVE_PATH_STRING          = "/js/articles_speech.js";
        const string SPEECH_ACCOUNT_PAGE_RELATIVE_PATH_STRING           = "/js/account.js";
        const string ADD_ARTICLES_RELATIVE_PATH_STRING                  = "/articles/add";
        const string MY_ARTICLES_RELATIVE_PATH_STRING                   = "/articles/my";
        const string MY_ARTICLES_SCRIPT_RELATIVE_PATH_STRING            = "/js/myarticles.js";
        const string REGISTER_NEW_USER_SCRIPT_RELATIVE_PATH_STRING      = "/js/register.js";
        const string REGISTER_NEW_USER_RELATIVE_PATH_STRING             = "/register";
        const string SEARCH_RELATIVE_PATH_STRING                        = "/search";
        const string SEARCH_SCRIPT_RELATIVE_PATH_STRING                 = "/js/search.js";
        const string LOGIN_USER_RELATIVE_PATH_STRING                    = "/login";
        const string LOGIN_SCRIPT_RELATIVE_PATH_STRING                  = "/js/login.js";
        const string ACCOUNT_RELATIVE_PATH_STRING                       = "/account";

        public static string getArtyomScriptPathString( string urlRelativePathString )
        {
            string urlPathString = SPEECH_MAIN_PAGE_RELATIVE_PATH_STRING;

            if( urlRelativePathString.Any(char.IsDigit) 
                && urlRelativePathString.Contains("articles")
                && urlRelativePathString.Contains("edit") )
            {
                urlRelativePathString = ADD_ARTICLES_RELATIVE_PATH_STRING;
            }

            switch( urlRelativePathString.ToLower() )
            {
                case ADD_ARTICLES_RELATIVE_PATH_STRING:
                    urlPathString = SPEECH_ARTICLES_PAGE_RELATIVE_PATH_STRING;
                    break;
                case ACCOUNT_RELATIVE_PATH_STRING:
                    urlPathString = SPEECH_ACCOUNT_PAGE_RELATIVE_PATH_STRING;
                    break;
                case LOGIN_USER_RELATIVE_PATH_STRING:
                    urlPathString = LOGIN_SCRIPT_RELATIVE_PATH_STRING;
                    break;
                case REGISTER_NEW_USER_RELATIVE_PATH_STRING:
                    urlPathString = REGISTER_NEW_USER_SCRIPT_RELATIVE_PATH_STRING;
                    break;
                case MY_ARTICLES_RELATIVE_PATH_STRING:
                    urlPathString = MY_ARTICLES_SCRIPT_RELATIVE_PATH_STRING;
                    break;
                case SEARCH_RELATIVE_PATH_STRING:
                    urlPathString = SEARCH_SCRIPT_RELATIVE_PATH_STRING;
                    break;
                default:
                    break;
            }

            return urlPathString;
        }
    }
}
