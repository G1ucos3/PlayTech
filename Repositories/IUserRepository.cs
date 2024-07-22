using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IUserRepository
    {
        void DeleteUser(User u);
        ObservableCollection<User> GetUser();
        User GetUserById(int id);
        void SaveUser(User u);
        void UpdateUser(User u);
        public User GetUserByEmail(string email);
        public User GetUserByUsername(string username);
    }
}