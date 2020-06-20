using System.Linq;
using YetAnotherPasswordChecker.BLL.Interfaces;

namespace YetAnotherPasswordChecker.BLL.PasswordRules
{
    public class ContainsUpperCase : IPasswordRule
    {
        public decimal Importance { get; set; }
        public bool Check(string input) => input.Any(char.IsUpper);

    }
}