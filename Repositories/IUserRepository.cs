using BusinessObjects;

namespace Repositories
{
    public interface IUserRepository
    {
        void DeleteUser(User u);
        List<User> GetUser();
        User GetUserById(int id);
        void SaveUser(User u);
        void UpdateUser(User u);
    }
}