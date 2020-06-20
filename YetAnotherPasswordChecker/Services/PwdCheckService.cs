using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using YetAnotherPasswordChecker.BLL;
using YetAnotherPasswordChecker.Models;

namespace YetAnotherPasswordChecker.Services
{
    #region Interface

    public interface IPwdCheckService
    {
        /// <summary>
        /// Checks passowrd for strength and returns <see cref="PasswordCheckResponse"/> if successful or a <see cref="PasswordErrorResponse"/> if anything goes wrong
        /// </summary>
        IActionResult CheckPassword(PasswordRequest password);
    }

    #endregion

    #region Implementation

    public class PwdCheckService : IPwdCheckService
    {
        #region Private Members

        private readonly IPwnedChecker _pwnedChecker;
        private readonly IPasswordCheckerService _passwordCheckerService;

        #endregion

        #region Constructor

        public PwdCheckService(IPwnedChecker pwnedChecker, IPasswordCheckerService passwordChecker)
        {
            _pwnedChecker = pwnedChecker;
            _passwordCheckerService = passwordChecker;
        }

        #endregion

        #region Public Methods

        public IActionResult CheckPassword(PasswordRequest password)
        {

            if (password == null)
            {
                return new JsonResult(new PasswordErrorResponse("Invalid Input", "Missing Input Data"))
                {
                    StatusCode = 400
                };
            }


            if (string.IsNullOrEmpty(password.Password))
            {
                return new JsonResult(new PasswordErrorResponse("Invalid Input", "The entered password string was empty"))
                {
                    StatusCode = 400
                };
            }

            var response = new PasswordCheckResponse();

            try
            {
                var isPwned = _pwnedChecker.CheckIfPwned(password.Password);

                response.IsPwned = isPwned;
            }
            catch
            {
                return new JsonResult(new PasswordErrorResponse("Server Error", "Cannot check if password is pwned."))
                {
                    StatusCode = 500
                };
            }

            try
            {
                var score = _passwordCheckerService.CheckStrength(password.Password);

                response.PwdStrength = score;
                response.PwdStrengthDescription = _passwordCheckerService.DescribeStrength(score);

            }
            catch
            {
                return new JsonResult(new PasswordErrorResponse("Server Error", "Cannot check if password strength."))
                {
                    StatusCode = 500
                };
            }

            return new JsonResult(response);
        }

        #endregion
    }
    #endregion
}

