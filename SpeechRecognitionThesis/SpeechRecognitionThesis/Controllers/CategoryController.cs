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
    [Route("Category")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IRespositoryWrapper _repositoryWrapper;

        public CategoryController(IRespositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Results")]
        public IActionResult GetAllCategory()
        {
            return Json(_repositoryWrapper.ArticlesCategory.FindAll().ToList());
        }
    }
}