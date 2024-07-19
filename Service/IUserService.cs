using BusinessObjects;
using System.Collections.ObjectModel;

namespace Service
{
    public interface IUserService
    {
        void DeleteUser(User u);
        ObservableCollection<User> GetUser();
        User GetUserById(int id);
        void SaveUser(User u);
        void UpdateUser(User u);
    }
}