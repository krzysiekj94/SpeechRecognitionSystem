using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Controllers
{
    [Route("Login")]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
    }

}
