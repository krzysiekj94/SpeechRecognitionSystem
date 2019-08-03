using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognitionThesis.Models;
using SpeechRecognitionThesis.Models.Repository;
using SpeechRecognitionThesis.Models.Scripts;
using SpeechRecognitionThesis.Models.ViewModels;

namespace SpeechRecognitionThesis.Controllers
{
    [Route("Register")]
    public class RegisterController : Controller
    {
        private readonly IRespositoryWrapper _repositoryWrapper;

        public RegisterController( IRespositoryWrapper repositoryWrapper )
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserModel userRegisterModel)
        {
            if( !ModelState.IsValid 
                || !ValidateRegisterUser(userRegisterModel) )
            {
                return BadRequest();
            }

            User registerUser = userRegisterModel.User;

            if( ProcessRegisterUserModelData(registerUser) )
            {
                _repositoryWrapper.Account.Add(registerUser);
            }

            return Ok();
        }

        private bool ProcessRegisterUserModelData(User registerUser)
        {
            return UpdateUserModelDates(registerUser) 
                && UserTools.ConvertPasswordToSha512(registerUser)
                && SetActiveFlagForRegisterUser(registerUser);
        }

        private bool SetActiveFlagForRegisterUser(User registerUser)
        {
            bool bSetActiveFlag = false;

            if(registerUser != null)
            {
                registerUser.ActiveAccountState = AccountActiveState.Active;
                bSetActiveFlag = true;
            }

            return bSetActiveFlag;
        }

        private bool UpdateUserModelDates(User registerUser)
        {
            bool bUpdateUserModelDates = false;

            if( registerUser != null )
            {
                registerUser.CreateAccountDate = DateTime.Now.ToString();
                registerUser.LastUpdateAccountDate = registerUser.CreateAccountDate;
                bUpdateUserModelDates = true;
            }

            return bUpdateUserModelDates;
        }

        private bool ValidateRegisterUser(RegisterUserModel userRegisterModel)
        {
            bool bValidateRegisterUser = true;

            if( IsNullFields(userRegisterModel)
                || IsFindUserByNickName(userRegisterModel) 
                || !IsEqualPasswords(userRegisterModel) )
            {
                bValidateRegisterUser = false;
            }

            return bValidateRegisterUser;
        }

        private bool IsNullFields(RegisterUserModel userRegisterModel)
        {
            return userRegisterModel.User.NickName == null
                || userRegisterModel.User.Password == null
                || userRegisterModel.ConfirmPassword == null
                || userRegisterModel.User.Email == null;
        }

        private bool IsEqualPasswords( RegisterUserModel findUserModel )
        {
            return (findUserModel.User.Password == findUserModel.ConfirmPassword);
        }

        private bool IsFindUserByNickName( RegisterUserModel userModel )
        {
            return (_repositoryWrapper.Account.FindAll()
                    .FirstOrDefault(findUser => findUser.NickName == userModel.User.NickName) != null);
        }
    }
}