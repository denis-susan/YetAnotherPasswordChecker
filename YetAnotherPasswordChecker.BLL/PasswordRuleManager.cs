using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YetAnotherPasswordChecker.BLL.Interfaces;
using YetAnotherPasswordChecker.DAL;
using YetAnotherPasswordChecker.DAL.Domain;

namespace YetAnotherPasswordChecker.BLL
{
    #region Interface
    public interface IPasswordRuleManager
    {
        /// <summary>
        /// Gets Executable active rules
        /// </summary>
        /// <returns>A list of <see cref="IPasswordRule"/></returns>
        List<IPasswordRule> GetActivePasswordRules();
    }

    #endregion

    #region Implementation

    public class PasswordRuleManager : IPasswordRuleManager
    {
        #region Private Members
        private readonly IPasswordRuleService _passwordRuleService;
        private readonly List<TypeInfo> _allTypes;
        #endregion

        #region Constructor

        public PasswordRuleManager(IPasswordRuleService passwordRuleService)
        {
            _passwordRuleService = passwordRuleService;
            _allTypes = Assembly
                .GetEntryAssembly()
                ?.GetReferencedAssemblies()
                .Select(Assembly.Load)
                .SelectMany(x => x.DefinedTypes)
                .ToList();
        }

        #endregion

        #region Public Methods

        public List<IPasswordRule> GetActivePasswordRules()
        {
            var dbRules = _passwordRuleService.GetActiveRules();

            return MapDbRules(dbRules);
        }

        #endregion

        #region Private Methods

        private List<IPasswordRule> MapDbRules(IList<PasswordRule> rules)
        {
            var items = new List<IPasswordRule>();
            var implementedRules = _allTypes.Where(x => x.GetInterface(nameof(IPasswordRule)) == typeof(IPasswordRule));

            foreach (var passwordRule in rules)
            {
                var rule = implementedRules?.FirstOrDefault(x => x.FullName.Equals(passwordRule.ImplementingType));

                if (rule != null)
                {
                    var instance = Activator.CreateInstance(rule) as IPasswordRule;

                    if (instance == null)
                        continue;

                    instance.Importance = passwordRule.Importance;

                    if (passwordRule.PasswordRuleConfigurations != null && rule.GetInterface(nameof(IPasswordRuleConfiguration)) == typeof(IPasswordRuleConfiguration))
                    {
                        var config = (IPasswordRuleConfiguration)instance;

                        var success = config.SetRuleConfiguration(passwordRule.PasswordRuleConfigurations);

                        if (!success) //if rule cannot configure itself it means db data is corrupted and we should skip it completely
                            continue;

                    }

                    items.Add(instance);
                }
            }

            return items;
        }

        #endregion
    }

    #endregion
}