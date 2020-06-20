using System;
using System.Collections.Generic;
using System.Linq;
using YetAnotherPasswordChecker.BLL.Interfaces;
using YetAnotherPasswordChecker.DAL.Domain;

namespace YetAnotherPasswordChecker.BLL.PasswordRules
{
    public class LengthValid : IPasswordRule, IPasswordRuleConfiguration
    {
        private int MaxLength { get; set; }
        private int MinLength { get; set; }
        public decimal Importance { get; set; }

        public bool Check(string input) => input.Length >= MinLength && input.Length <= MaxLength;
        
        public bool SetRuleConfiguration(IList<PasswordRuleConfiguration> configuration)
        {
            var maxLength = configuration.SingleOrDefault(x => x.Name == nameof(MaxLength));
            var minLength = configuration.SingleOrDefault(x => x.Name == nameof(MinLength));

            if (maxLength != null)
                MaxLength = Convert.ToInt32(maxLength.Value);
            else return false;


            if (minLength != null)
                MinLength = Convert.ToInt32(minLength.Value);
            else return false;


            return true;
        }
    }
}