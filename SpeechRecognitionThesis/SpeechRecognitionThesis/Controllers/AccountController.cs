using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognitionThesis.Models.Repository;

namespace SpeechRecognitionThesis.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IRespositoryWrapper _repositoryWrapper;

        public AccountController( IRespositoryWrapper repositoryWrapper )
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult GetMainAccountView()
        {
            return View( "AccountGeneral" );
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult GetEditAccountView()
        {
            return View( "AccountEdit" );
        }

        [HttpGet]
        [Route("Articles")]
        public IActionResult GetArticlesAccountView()
        {
            return View( "AccountArticles" );
        }
    }
}