using System.Collections.Generic;
using System.Linq;
using WorkoutTracker_v2.Helpers;
using WorkoutTracker_v2.Models;

namespace WorkoutTracker_v2.Repositories
{
    public interface IUserRepository
    {
        UserProfile GetUsernameAndPassword(string username, string password);
    }


    public class UserRepository : IUserRepository
    {
        // This needs to be Data'based
        private List<UserProfile> users = new List<UserProfile>
        {
            new UserProfile { UserId = 1, UserName = "Car-Key", isActive = true, PassWord = "RsZpbkHqf+jmXUPa8ITyOb1YU1vt/MpsWioKcVM6aUI=" }
        };

        public UserProfile GetUsernameAndPassword(string username, string password)
        {
            var test = password.Sha256();
            var user = users.SingleOrDefault(u => u.UserName == username && u.PassWord == password.Sha256());
            return user;
        }

    }
}
