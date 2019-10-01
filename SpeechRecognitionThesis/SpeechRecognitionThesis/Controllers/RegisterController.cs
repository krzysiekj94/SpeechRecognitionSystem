using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognitionThesis.Models;
using SpeechRecognitionThesis.Models.Repository;
using SpeechRecognitionThesis.Models.Scripts;
using SpeechRecognitionThesis.Models.ViewModels;

namespace SpeechRecognitionThesis.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Register")]
    public class RegisterController : Controller
    {
        private readonly IRespositoryWrapper _repositoryWrapper;

        public RegisterController( IRespositoryWrapper repositoryWrapper )
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromForm] RegisterUserModel userRegisterModel)
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
                _repositoryWrapper.Save();
                
                TokenProvider tokenProvider = new TokenProvider();
                string userTokenString = tokenProvider.CreateUserTokenString(registerUser);

                if (userTokenString != null
                    && userTokenString.Length > 0)
                {
                    HttpContext.Session.SetString(TokenProvider.GetTokenSessionKeyString(), userTokenString);
                }
            }

            return Ok();
        }

        private bool ProcessRegisterUserModelData(User registerUser)
        {
            return DeleteWhiteSpaces(registerUser)
                && UpdateUserModelDates(registerUser) 
                && UserTools.ConvertPasswordToSha512(registerUser)
                && SetActiveFlagForRegisterUser(registerUser);
        }

        private bool DeleteWhiteSpaces(User registerUser)
        {
            registerUser.NickName = registerUser.NickName.Trim();
            registerUser.Password = registerUser.Password.Trim();
            registerUser.Email = registerUser.Email.Trim();

            return true;
        }

        private bool SetActiveFlagForRegisterUser(User registerUser)
        {
            bool bSetActiveFlag = false;

            if(registerUser != null)
            {
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
                registerUser.LastUpdateAccountDate = DateTime.Now.ToString();
                registerUser.LastLoggedAccountDate = DateTime.Now.ToString();
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