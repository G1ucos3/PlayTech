﻿using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public ObservableCollection<User> GetUser() => UserDAO.GetUser();

        public void SaveUser(User u) => UserDAO.SaveUser(u);

        public void UpdateUser(User u) => UserDAO.UpdateUser(u);

        public void DeleteUser(User u) => UserDAO.DeleteUser(u);

        public User GetUserById(int id) => UserDAO.GetUserById(id);

        public User GetUserByEmail(string email) => UserDAO.GetUserByEmail(email);

        public User GetUserByUsername(string username) => UserDAO.GetUserByUsername(username);
    }
}
