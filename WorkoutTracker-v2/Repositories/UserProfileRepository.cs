using System.Collections.Generic;
using System.Linq;
using WorkoutTracker_v2.Helpers;
using WorkoutTracker_v2.Models;
using WorkoutTracker_v2.Services;

namespace WorkoutTracker_v2.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetUsernameAndPassword(string username, string password);
        UserProfile GetUserByEmail(string googleId);
        IEnumerable<UserProfile> GetUsers();
        void InsertUser(string username, string email);
    }


    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IDapperService _dapperService;
        public UserProfileRepository(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        public UserProfile GetUsernameAndPassword(string username, string password)
        {
            var convertedPassword = password.Sha256();
            var query = @"select * from UserProfile where UserName = @username and PassWord = @password";
            var user = _dapperService.ExecuteQuery<UserProfile>(query, new { username, password = convertedPassword });
            return user.FirstOrDefault();
        }

        public UserProfile GetUserByEmail(string email)
        {
            var query = @"SELECT * FROM UserProfile WHERE Email = @email";
            var user = _dapperService.ExecuteQuery<UserProfile>(query, new { email });
            return user.FirstOrDefault();
        }

        public IEnumerable<UserProfile> GetUsers()
        {
            var query = "SELECT * FROM UserProfile";
            var users = _dapperService.ExecuteQuery<UserProfile>(query);
            return users;
        }

        public void InsertUser(string username, string email)
        {
            var query = @"INSERT INTO USERPROFILE(username, isactive, email) values (@username, 1, @email)";
            _dapperService.Insert(query, new { username, email });
        }
    }
}
