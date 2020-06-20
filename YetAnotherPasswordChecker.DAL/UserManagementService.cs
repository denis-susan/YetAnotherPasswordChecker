using System.Linq;
using YetAnotherPasswordChecker.DAL.Domain;

namespace YetAnotherPasswordChecker.DAL
{
    #region Interface
    public interface IUserManagementService
    {
        /// <summary>
        /// Gets the user based on username and password 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User GetUser(string username, string password);

        /// <summary>
        /// Adds a new user to the users table.
        /// </summary>
        /// <param name="user"></param>
        void AddUser(User user);
    }
    #endregion


    #region Implementation

    public class UserManagementService : IUserManagementService
    {
        private readonly PasswordContext _context;
        public UserManagementService(PasswordContext context)
        {
            _context = context;
        }

        public User GetUser(string username, string password)
            => _context.Users.FirstOrDefault(x => x.Name.Equals(username));

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

    }

    #endregion
}