using BusinessObjects;

namespace Service
{
    public interface IUserService
    {
        void DeleteUser(User u);
        List<User> GetUser();
        User GetUserById(int id);
        void SaveUser(User u);
        void UpdateUser(User u);
    }
}