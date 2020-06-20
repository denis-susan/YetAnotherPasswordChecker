using System.Collections.Generic;

namespace YetAnotherPasswordChecker.DAL.Domain
{
    public class PasswordRule
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public short Importance { get; set; }
        public string ImplementingType { get; set; }
        public List<PasswordRuleConfiguration> PasswordRuleConfigurations { get; set; }
    }
}