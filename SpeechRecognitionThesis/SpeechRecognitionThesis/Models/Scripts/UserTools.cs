using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Scripts
{
    public class UserTools
    {
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

        static bool AuthenticateUserProcess(User loginUser)
        {
            return (loginUser != null)
                    && UserTools.ConvertPasswordToSha512(loginUser);
        }
    }
}
