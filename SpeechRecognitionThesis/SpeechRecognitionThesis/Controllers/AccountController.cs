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
    }
}