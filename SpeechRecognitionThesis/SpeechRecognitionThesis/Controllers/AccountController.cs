﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognitionThesis.Models;
using SpeechRecognitionThesis.Models.DatabaseModels;
using SpeechRecognitionThesis.Models.Repository;
using SpeechRecognitionThesis.Models.Scripts;
using SpeechRecognitionThesis.Models.ViewModels;

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
            AccountUserModel userAccountModel = new AccountUserModel();

            SetLoggedUserForAccountUserModel( userAccountModel );
            SetLoggedUserInfoArticles(userAccountModel);

            return View( "AccountGeneral", userAccountModel);
        }

        private void SetLoggedUserInfoArticles(AccountUserModel userAccountModel)
        {
            long lUserId = -1;
            IEnumerable<UserArticles> userArticlesList = null;

            if (userAccountModel != null
                && long.TryParse(TokenProvider.GetRegisterUserPropertyString(User.Identity, UserTools.USER_ID_PROPERTY_STRING), out lUserId))
            {
                userArticlesList = _repositoryWrapper.UserArticles.GetUserArticles(lUserId);
                userAccountModel.iAmountOfArticles = userArticlesList.Count();
            }
        }

        private void SetLoggedUserForAccountUserModel(AccountUserModel userAccountModel)
        {
            long lUserId = -1;
            User loggedUser = null;

            if (userAccountModel != null 
                && long.TryParse(TokenProvider.GetRegisterUserPropertyString(User.Identity, UserTools.USER_ID_PROPERTY_STRING), out lUserId))
            {
                loggedUser = _repositoryWrapper.Account.GetUser(lUserId);
                loggedUser.Password = string.Empty;
                loggedUser.Id = -1;

                userAccountModel.User = loggedUser;
            }
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

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteUserAccount()
        {
            long lUserId = TokenProvider.GetLoggedUserId(User.Identity);
            IEnumerable<UserArticles> userArticlesEnumerable = null;

            if( lUserId != -1 )
            {
                userArticlesEnumerable = _repositoryWrapper.UserArticles.DeleteArticles(lUserId);
                _repositoryWrapper.Articles.DeleteArticles(userArticlesEnumerable);
                _repositoryWrapper.Account.Delete(new User { Id = lUserId } );
                _repositoryWrapper.Save();
                HttpContext.Session.Clear();
            }
            
            return Ok();
        }

        [HttpPost]
        [Route("Change")]
        public IActionResult ChangeUserDataAccount( [FromForm] RegisterUserModel userChangeModel )
        {
            if( userChangeModel == null )
            {
                return BadRequest();
            }
            else if( userChangeModel.ConfirmPassword != userChangeModel.User.Password )
            {
                return BadRequest("Hasło i jego potwierdzenie nie są zgodne!");
            }

            long lUserId = TokenProvider.GetLoggedUserId( User.Identity );

            if( lUserId != -1 
                && _repositoryWrapper.Account.UpdateUserData( lUserId, userChangeModel.User ) )
            {
                _repositoryWrapper.Save();
            }

            return Ok();
        }
    }
}