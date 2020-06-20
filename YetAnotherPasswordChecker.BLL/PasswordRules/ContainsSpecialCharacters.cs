using System.Linq;
using YetAnotherPasswordChecker.BLL.Interfaces;

namespace YetAnotherPasswordChecker.BLL.PasswordRules
{
    public class ContainsSpecialCharacters : IPasswordRule
    {
        public decimal Importance { get; set; }
        public bool Check(string input) => input.Any(x=> !char.IsLetterOrDigit(x));
    }
}