﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly IRespositoryWrapper _repositoryWrapper;

        public LoginController(IRespositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login( [FromForm] LoginUserModel userLoginModel )
        {
            if (!ModelState.IsValid
                || !ValidateLoginUser(userLoginModel))
            {
                return BadRequest();
            }

            User loginUser = userLoginModel.User;
            User loggedUser = null;
            string userSessionDataString = string.Empty;

            if( ProcessLoginUserModelData(loginUser) )
            {
                loggedUser = await _repositoryWrapper.Account.Authenticate( loginUser.NickName, loginUser.Password );

                if (loggedUser == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                else
                {
                    userSessionDataString = Guid.NewGuid().ToString();

                    if( SaveUserSessionInfoInDB(loginUser.NickName, userSessionDataString ) )
                    {
                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                         new ClaimsPrincipal(PrepareClaimsIdentity(userSessionDataString)),
                        new AuthenticationProperties());
                    }

                    return Ok(loggedUser);
                }
            }

            return Ok();
        }

        private bool SaveUserSessionInfoInDB( string nickNameString, string userSessionDataString )
        {
            User findUser = _repositoryWrapper.Account.FindAll()
                        .FirstOrDefault(resultUser=> resultUser.NickName == nickNameString);

            bool bSaveUserSessionData = false;

            if(findUser != null)
            {
                _repositoryWrapper.UserSessions
                    .Add(new UserSession()
                    {
                        UserId = findUser.UserId,
                        SessionData = userSessionDataString,
                    });

                bSaveUserSessionData = true;
            }

            return bSaveUserSessionData;
        }

        private ClaimsIdentity PrepareClaimsIdentity( string userSessionDataString )
        {
            List<Claim> claimList = new List<Claim>
            {
                new Claim( ClaimTypes.Name, userSessionDataString )
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claimList, CookieAuthenticationDefaults.AuthenticationScheme);

            return claimsIdentity;
        }

        private bool ProcessLoginUserModelData(User loginUser)
        {
            return (loginUser != null);
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
            return (_repositoryWrapper.Account.FindAll()
                .FirstOrDefault(findUser => findUser.NickName == userLoginModel.User.NickName) != null);
        }

        private bool IsNullFields(LoginUserModel userLoginModel)
        {
            return userLoginModel.User.NickName == null
                || userLoginModel.User.Password == null;
        }
    }
}