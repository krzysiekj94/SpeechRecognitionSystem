using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Scripts
{
    public class CommandsManager
    {
        const string MAIN_PAGE_PATH = "/";
        const string SEARCH_PAGE_PATH = "/search";
        const string LOGIN_PAGE_PATH = "/login";
        const string REGISTER_PAGE_PATH = "/register";
        const string ARTICLE_PAGE_PATH = "/articles/add";


        const string SEARCH_COMMANDS_PARTIAL_VIEW_PATH = "_SearchEngineCommandsPartialView";
        const string MAIN_COMMANDS_PARTIAL_VIEW_PATH = "_MainCommandsPartialView";
        const string REGISTER_COMMANDS_PARTIAL_VIEW_PATH = "_RegisterCommandsPartialView";
        const string LOGIN_COMMANDS_PARTIAL_VIEW_PATH = "_LoginCommandsPartialView";
        const string ARTICLE_COMMANDS_PARTIAL_VIEW_PATH = "_ArticleCommandsPartialView";

        public static string GetCommandsPartialViewPathString( string oPagePathString)
        {
            string commandsPartialViewNameString = MAIN_COMMANDS_PARTIAL_VIEW_PATH;

            switch( oPagePathString )
            {
                case SEARCH_PAGE_PATH:
                    commandsPartialViewNameString = SEARCH_COMMANDS_PARTIAL_VIEW_PATH;
                    break;
                case LOGIN_PAGE_PATH:
                    commandsPartialViewNameString = LOGIN_COMMANDS_PARTIAL_VIEW_PATH;
                    break;
                case REGISTER_PAGE_PATH:
                    commandsPartialViewNameString = REGISTER_COMMANDS_PARTIAL_VIEW_PATH;
                    break;
                case ARTICLE_PAGE_PATH:
                    commandsPartialViewNameString = ARTICLE_COMMANDS_PARTIAL_VIEW_PATH;
                    break;
                default:
                    break;
            }

            return "CommandsPartialView/" + commandsPartialViewNameString + ".cshtml";
        }
    }
}
