using BusinessObjects;
using DataAccessLayer;
using Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iUserRepository;

        public UserService()
        {
            iUserRepository = new UserRepository();
        }

        public ObservableCollection<User> GetUser() => iUserRepository.GetUser();

        public void SaveUser(User u) => iUserRepository.SaveUser(u);

        public void UpdateUser(User u) => iUserRepository.UpdateUser(u);

        public void DeleteUser(User u) => iUserRepository.DeleteUser(u);

        public User GetUserById(int id) => iUserRepository.GetUserById(id);

        public User GetUserByEmail(string email) => iUserRepository.GetUserByEmail(email);

        public User GetUserByUsername(string username) => iUserRepository.GetUserByUsername(username);
    }
}
