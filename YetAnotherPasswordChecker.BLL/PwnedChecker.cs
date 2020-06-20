using System.Linq;
using System.Security.Cryptography;
using System.Text;
using YetAnotherPasswordChecker.DAL;

namespace YetAnotherPasswordChecker.BLL
{
    #region Interface

    public interface IPwnedChecker
    {
        /// <summary>
        /// checks if the supplied password is pwned or not
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CheckIfPwned(string password);
    }

    #endregion

    #region Implementation

    public class PwnedChecker : IPwnedChecker
    {
        #region Private Members

        private readonly IHaveIBeenPwnedService _service;

        #endregion

        #region Constructor

        public PwnedChecker(IHaveIBeenPwnedService service)
        {
            _service = service;
        }

        #endregion

        #region Public Methods

        public bool CheckIfPwned(string password)
        {
            var passwordHash = Hash(password);
            var range = passwordHash.Substring(0, 5);

            var passwords = _service.SearchByRange(range);

            return passwords.Any(x=> x.StartsWith(passwordHash.Remove(0,5)));
        }

        #endregion

        #region Private Methods

        private string Hash(string input)
        {
            using var sha1 = new SHA1Managed();

            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (var b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        #endregion
    }

    #endregion
}