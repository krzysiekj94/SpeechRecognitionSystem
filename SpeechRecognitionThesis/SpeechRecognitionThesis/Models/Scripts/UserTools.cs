using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Scripts
{
    public class UserTools
    {
        public static readonly string ANONYMOUS_USER_NICKNAME = "Guest";
        public static readonly string REGISTER_DATE_PROPERTY_STRING = "RegisterDateString";
        public static readonly string USER_LAST_LOGGED_DATE_PROPERTY_STRING = "LastLoggedDateString";
        public static readonly string USER_ID_PROPERTY_STRING = "UserId";
        public static readonly string USER_EMAIL_PROPERTY_STRING = "Email";
        public static readonly string URL_WEBSITE_STRING = "http://localhost:8080/";

        static public bool ConvertPasswordToSha512(User registerUser)
        {
            bool bConvertPasswordResult = false;
            string hashPasswordString = string.Empty;
            string passwordString = registerUser.Password;

            if (passwordString.Length > 0)
            {
                hashPasswordString = Sha512Manager.ComputeHashString(registerUser.Password);

                if (hashPasswordString.Length > 0)
                {
                    registerUser.Password = hashPasswordString;
                    bConvertPasswordResult = true;
                }
            }

            return bConvertPasswordResult;
        }

        static public string ConvertInputTextToSha512(string inputTextString )
        {
            return Sha512Manager.ComputeHashString(inputTextString);
        }

        static bool AuthenticateUserProcess(User loginUser)
        {
            return (loginUser != null)
                    && UserTools.ConvertPasswordToSha512(loginUser);
        }
    }
}
