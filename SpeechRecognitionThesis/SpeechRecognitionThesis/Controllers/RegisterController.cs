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
        public IActionResult Register(RegisterModel userRegisterModel)
        {
            if(userRegisterModel == null)
            {
                return BadRequest();
            }

            _dataRepository.Add(userRegisterModel.User);

            return Ok();
        }
    }
}