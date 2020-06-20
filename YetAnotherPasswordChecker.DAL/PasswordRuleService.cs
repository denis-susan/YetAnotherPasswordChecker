using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using YetAnotherPasswordChecker.DAL.Domain;

namespace YetAnotherPasswordChecker.DAL
{
    #region Interface
    public interface IPasswordRuleService
    {
        /// <summary>
        /// Gets Active Rules from the database layer
        /// </summary>
        /// <returns>List of <see cref="PasswordRule"/></returns>
        IList<PasswordRule> GetActiveRules();
    }
    #endregion

    #region Implementation

    public class PasswordRuleService : IPasswordRuleService
    {
        private readonly PasswordContext _context;


        public PasswordRuleService(PasswordContext context)
        {
            _context = context;
        }

        public IList<PasswordRule> GetActiveRules()
           => _context.PasswordRules.Where(x => x.IsActive)
                                    .Include(x => x.PasswordRuleConfigurations)
                                    .ToList();
    }
    #endregion
}