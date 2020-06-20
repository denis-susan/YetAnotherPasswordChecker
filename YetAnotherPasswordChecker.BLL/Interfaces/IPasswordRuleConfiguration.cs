using System.Collections;
using System.Collections.Generic;
using YetAnotherPasswordChecker.DAL.Domain;

namespace YetAnotherPasswordChecker.BLL.Interfaces
{
    public interface IPasswordRuleConfiguration
    {
        /// <summary>
        /// Provides a way for the implementing rule to configure itself
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        bool SetRuleConfiguration(IList<PasswordRuleConfiguration> configuration);
    }
}