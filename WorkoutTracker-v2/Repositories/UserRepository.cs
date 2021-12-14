using System.Collections.Generic;
using System.Linq;
using WorkoutTracker_v2.Helpers;
using WorkoutTracker_v2.Models;
using WorkoutTracker_v2.Services;

namespace WorkoutTracker_v2.Repositories
{
    public interface IUserRepository
    {
        UserProfile GetUsernameAndPassword(string username, string password);
        UserProfile GetUserByEmail(string googleId);
        void InsertUser(string userName, string email);
    }


    public class UserRepository : IUserRepository
    {
        private readonly IDapperService _dapperService;   
        public UserRepository(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        /* This needs to be Data'based
        private List<UserProfile> users = new List<UserProfile>
        {
            new UserProfile { UserId = 1, UserName = "Car-Key", isActive = true, PassWord = "RsZpbkHqf+jmXUPa8ITyOb1YU1vt/MpsWioKcVM6aUI=", Email = "sarthak.npl@gmail.com" }
        };
        */

        public UserProfile GetUsernameAndPassword(string username, string password)
        {
            var convertedPassword = password.Sha256();
            var user = _dapperService.Get<UserProfile>($"select * from UserProfile where UserName = '{username}' and PassWord = '{convertedPassword}'", null, System.Data.CommandType.Text);
            // var user = users.FirstOrDefault(u => u. == username && u.PassWord == password.Sha256());
            return user;
        }

        public UserProfile GetUserByEmail(string email)
        {
            // return users.FirstOrDefault(x => x.Email == googleId);
            var user = _dapperService.Get<UserProfile>($"select * from UserProfile where Email = '{email}'", null, System.Data.CommandType.Text);
            // var user = users.FirstOrDefault(u => u. == username && u.PassWord == password.Sha256());
            return user;
        }

        public void InsertUser(string userName, string email)
        {

        }

    }
}
