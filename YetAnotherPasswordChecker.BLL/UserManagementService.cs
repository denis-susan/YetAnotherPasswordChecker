using System;
using YetAnotherPasswordChecker.BLL.DTO;
using YetAnotherPasswordChecker.DAL.Domain;

namespace YetAnotherPasswordChecker.BLL
{
    using DAL = YetAnotherPasswordChecker.DAL.IUserManagementService; //scoped using because using fqdn is ugly

    #region Interface

    public interface IUserManagementService
    {
        /// <summary>
        /// Authenticates user using username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserDto Authenticate(string username, string password);
    }

    #endregion

    #region Implementation

    public class UserManagementService : IUserManagementService
    {
        #region Private Members

        private readonly DAL _userManagementServiceDal;

        #endregion

        #region Constructor

        public UserManagementService(DAL dal)
        {
            _userManagementServiceDal = dal;
        }

        #endregion

        #region Public Methods

        public UserDto Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new Exception("Username is empty!");

            if (string.IsNullOrEmpty(password))
                throw new Exception("Password is empty!");

            var user = _userManagementServiceDal.GetUser(username, password);

            if (user != null && !user.Password.Equals(password))
                throw new Exception($"Invalid password for user: {username}");


            // this is here for the sole purpose of this test and it breaks single responsibility on purpose 
            // because it simplifies the method of adding a user for testing purposes, also adding users is out of scope in the test app.

            if (user == null)
            {
                user = new User()
                {
                    Name = username,
                    Password = password
                };

                _userManagementServiceDal.AddUser(user);
            }
           
            return new UserDto()
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        #endregion
    }

    #endregion
}