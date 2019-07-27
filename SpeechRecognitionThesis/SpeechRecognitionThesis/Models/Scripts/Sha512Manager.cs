using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Scripts
{
    public class Sha512Manager
    {
        static public string ComputeHashString(string inputString)
        {
            byte[] sha512HashByteArray = null;
            string sha512HashString = string.Empty;

            if(inputString.Length > 0)
            {
                using(var sha512 = SHA512.Create())
                {
                    sha512HashByteArray = sha512.ComputeHash(Encoding.UTF8.GetBytes(inputString));
                    sha512HashString = BitConverter.ToString(sha512HashByteArray).Replace("-", "").ToLower();
                }
            }

            return sha512HashString;
        }
    }
}
