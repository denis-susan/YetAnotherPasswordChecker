using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using YetAnotherPasswordChecker.BLL.Interfaces;

namespace YetAnotherPasswordChecker.BLL
{
    #region Interface

    public interface IPasswordCheckerService
    {
        /// <summary>
        /// Checks the strength of the provided password against a weighted calculation.
        /// </summary>
        /// <param name="password">The password string</param>
        /// <returns>A number from 0 to 10</returns>
        int CheckStrength(string password);

        /// <summary>
        /// Prints out information about a specific password strength
        /// </summary>
        /// <param name="strength"></param>
        /// <returns></returns>
        string DescribeStrength(int strength);
    }

    #endregion

    #region Implementation

    public class PasswordCheckerService : IPasswordCheckerService
    {
        #region Private Members

        private readonly IPasswordRuleManager _passwordRuleManager;

        #endregion

        #region Constructor

        public PasswordCheckerService(IPasswordRuleManager passwordRuleManager)
        {
            _passwordRuleManager = passwordRuleManager;
        }

        #endregion

        #region Public Methods

        public int CheckStrength(string password)
        {
            var rules = _passwordRuleManager.GetActivePasswordRules();

            // Formula is essentially Y = P% * X where Y is the importance and X is the sum of importances

            decimal total = rules.Sum(x => x.Importance);

            decimal computedResult = 0;

            foreach (var passwordRule in rules)
            {
                var share = (passwordRule.Importance / total) * 100;

                if (passwordRule.Check(password))
                    computedResult += share;

            }

            //business requirement
            if (computedResult == 0)
                computedResult = 1;

            //result is a percentage, we divide by 10 to get a representation from 1 to 10

            return Convert.ToInt32(computedResult) / 10;
        }

        public string DescribeStrength(int strength)
        {
            switch (strength)
            {
                case 1:
                case 2:
                case 3:
                    return "Password is extremely weak!";

                case 4:
                case 5:
                    return "Password is weak";

                case 6:
                case 7:
                    return "Password has medium security";

                case 8:
                case 9:
                    return "Password strength is ok";

                case 10:
                    return "Password is secure!";

                default:
                    return "Password strength cannot be determined at this time";
            }
        }

        #endregion
    }

    #endregion
}