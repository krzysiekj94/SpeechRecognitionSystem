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
    [Route("Logout")]
    public class LogoutController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public LogoutController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Logout()
        {
            if(User.Identity.IsAuthenticated)
            {
                HttpContext.Session.Clear();
            }

            return Redirect("/");
        }
    }
}