using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognitionThesis.Models;
using SpeechRecognitionThesis.Models.Repository;
using SpeechRecognitionThesis.Models.Scripts;
using SpeechRecognitionThesis.Models.ViewModels;

namespace SpeechRecognitionThesis.Controllers
{
    //[Authorize]
    //[Route("Login")]
    public class LoginController : Controller
    {
        private readonly IDataRepository<User> _dataRepository;

        public LoginController(IDataRepository<User> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginUserModel userLoginModel)
        {
            if (!ModelState.IsValid
                || !ValidateLoginUser(userLoginModel))
            {
                return BadRequest();
            }

            User loginUser = userLoginModel.User;

            if( ProcessLoginUserModelData(loginUser) )
            {
                //#TODO
            }

            return Ok();
        }

        private bool ProcessLoginUserModelData(User loginUser)
        {
            return (loginUser != null) 
                 && UserTools.ConvertPasswordToSha512(loginUser);
        }

        private bool ValidateLoginUser(LoginUserModel userLoginModel)
        {
            bool bValidateRegisterUser = true;

            if( IsNullFields(userLoginModel)
                || !IsFindUserByNickName(userLoginModel))
            {
                bValidateRegisterUser = false;
            }

            return bValidateRegisterUser;
        }

        private bool IsFindUserByNickName(LoginUserModel userLoginModel)
        {
            return (_dataRepository.GetAll()
                .FirstOrDefault(findUser => findUser.NickName == userLoginModel.User.NickName) != null);
        }

        private bool IsNullFields(LoginUserModel userLoginModel)
        {
            return userLoginModel.User.NickName == null
                || userLoginModel.User.Password == null;
        }
    }
}