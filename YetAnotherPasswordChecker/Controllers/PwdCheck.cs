using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using YetAnotherPasswordChecker.BLL;
using YetAnotherPasswordChecker.DAL;
using YetAnotherPasswordChecker.Models;
using YetAnotherPasswordChecker.Services;

namespace YetAnotherPasswordChecker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PwdCheck : ControllerBase
    {
        #region Private members

        private readonly IPwdCheckService _pwdCheckService;
        private readonly IMemoryCache _cache;

        #endregion

        #region Constructor

        public PwdCheck(IPwdCheckService pwdCheckService, IMemoryCache cache)
        {
            _pwdCheckService = pwdCheckService;
            _cache = cache;
        }

        #endregion

        #region Public Members

        [HttpPost]
        public IActionResult Post([FromBody] PasswordRequest password)
        {
            if (_cache.TryGetValue(password?.Password, out var response))
            {
                return response as IActionResult;
            }
            else
            {
                response = _pwdCheckService.CheckPassword(password);

                _cache.Set(password?.Password, response);

                return response as IActionResult;
            }

        }

        #endregion
    }
}
