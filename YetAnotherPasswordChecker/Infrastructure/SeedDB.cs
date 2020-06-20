using System.Collections.Generic;
using YetAnotherPasswordChecker.DAL;
using YetAnotherPasswordChecker.DAL.Domain;

namespace YetAnotherPasswordChecker.Infrastructure
{
    public class SeedDB
    {
        // note: this class is only used here in order to simplify the test app deployment, in a real prod environment we would use migrations
        public static void Seed(PasswordContext ctx)
        {
            var rules = new List<PasswordRule>()
            {
                new PasswordRule()
                {
                    Name = "Contains Numbers",
                    ImplementingType = "YetAnotherPasswordChecker.BLL.PasswordRules.ContainsNumbers",
                    IsActive = true,
                    Importance = 1
                },
                new PasswordRule()
                {
                    Name = "Contains Special Characters",
                    ImplementingType = "YetAnotherPasswordChecker.BLL.PasswordRules.ContainsSpecialCharacters",
                    IsActive = true,
                    Importance = 2
                },
                new PasswordRule()
                {
                    Name = "Contains Upper Case",
                    ImplementingType = "YetAnotherPasswordChecker.BLL.PasswordRules.ContainsUpperCase",
                    IsActive = true,
                    Importance = 1
                },
                new PasswordRule()
                {
                    Name = "Length Valid",
                    ImplementingType = "YetAnotherPasswordChecker.BLL.PasswordRules.LengthValid",
                    IsActive = true,
                    Importance = 4,
                    PasswordRuleConfigurations = new List<PasswordRuleConfiguration>()
                    {
                        new PasswordRuleConfiguration()
                        {
                            Name = "MaxLength",
                            Value = "100"
                        },
                        new PasswordRuleConfiguration()
                        {
                            Name = "MinLength",
                            Value = "10"
                        }
                    }
                },
            };

            ctx.PasswordRules.AddRange(rules);
            ctx.SaveChanges();
        }
    }
}