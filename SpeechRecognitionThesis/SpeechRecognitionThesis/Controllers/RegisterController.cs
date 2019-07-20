using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognitionThesis.Models;
using SpeechRecognitionThesis.Models.Repository;
using SpeechRecognitionThesis.Models.ViewModels;

namespace SpeechRecognitionThesis.Controllers
{
    [Route("Register")]
    public class RegisterController : Controller
    {
        private readonly IDataRepository<User> _dataRepository;

        public RegisterController(IDataRepository<User> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserModel userRegisterModel)
        {
            if( !ModelState.IsValid )
            {
                return BadRequest();
            }

            ValidateRegisterUser(userRegisterModel);

            _dataRepository.Add(userRegisterModel.User);

            return Ok();
        }

        private bool ValidateRegisterUser(RegisterUserModel userRegisterModel)
        {
            bool bValidateRegisterUser = true;

            User findUser = userRegisterModel.User;

            if( IsFindUserByNickName(findUser) )
            {
                bValidateRegisterUser = false;
            }

            return bValidateRegisterUser;
        }

        private bool IsFindUserByNickName(User user)
        {
            return (_dataRepository.GetAll()
                    .FirstOrDefault(findUser => findUser.NickName == user.NickName) != null);
        }
    }
}