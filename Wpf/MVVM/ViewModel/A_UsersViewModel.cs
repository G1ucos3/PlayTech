using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Wpf.MVVM.View;

namespace Wpf.MVVM.ViewModel
{
    class A_UsersViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private readonly ICurrentComputerService _currentComputerService;
        private readonly IComputerService _computerService;
        private ObservableCollection<User> users;

        private int _totalUsers;
        private int _totalGamers;
        private int _totalManagers;

        public int TotalUsers
        {
            get { return _totalUsers; }
            set
            {
                _totalUsers = value;
                OnPropertyChanged(nameof(TotalUsers));
            }
        }

        public int TotalGamers
        {
            get { return _totalGamers; }
            set
            {
                _totalGamers = value;
                OnPropertyChanged(nameof(TotalGamers));
            }
        }

        public int TotalManagers
        {
            get { return _totalManagers; }
            set
            {
                _totalManagers = value;
                OnPropertyChanged(nameof(TotalManagers));
            }
        }

        public ObservableCollection<User> Users
        {
            get => users;
            set
            {
                if(users != value)
                {
                    users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        public A_UsersViewModel() { }

        public A_UsersViewModel(IUserService userService, ICurrentComputerService currentComputerService, IComputerService computerService)
        {
            _userService = userService;
            _currentComputerService = currentComputerService;
            _computerService = computerService;
            loadUser();
        }

        public void loadUser()
        {
            var list = new ObservableCollection<User>();
            var listUser = _userService.GetUser();
            int gamers = 0;
            int managers = 0;
            foreach (var user in listUser)
            {
                if (user.UserRoles == 1) continue;
                if (user.UserRoles == 2) managers++;
                if (user.UserRoles == 3) gamers++;
                user.CurrentComputers = _currentComputerService.GetCurrentComputerByUserID(user.UserId).ToList();
                foreach (var computer in user.CurrentComputers)
                {
                    computer.Computer = _computerService.GetComputerById(computer.ComputerId);
                }
                list.Add(user);
            }
            TotalUsers = list.Count;
            TotalGamers = gamers;
            TotalManagers = managers;
            Users = list;
        }

        public void loadGamers()
        {
            var list = new ObservableCollection<User>();
            var listUser = _userService.GetUser();
            foreach (var user in listUser)
            {
                if (user.UserRoles == 1 || user.UserRoles == 2) continue;
                user.CurrentComputers = _currentComputerService.GetCurrentComputerByUserID(user.UserId).ToList();
                foreach (var computer in user.CurrentComputers)
                {
                    computer.Computer = _computerService.GetComputerById(computer.ComputerId);
                }
                list.Add(user);
            }
            Users = list;
        }

        public void loadManagers()
        {
            var list = new ObservableCollection<User>();
            var listUser = _userService.GetUser();
            foreach (var user in listUser)
            {
                if (user.UserRoles == 1 || user.UserRoles == 3) continue;
                user.CurrentComputers = _currentComputerService.GetCurrentComputerByUserID(user.UserId).ToList();
                foreach (var computer in user.CurrentComputers)
                {
                    computer.Computer = _computerService.GetComputerById(computer.ComputerId);
                }
                list.Add(user);
            }
            Users = list;
        }

        public void createUser(User user)
        {
            _userService.SaveUser(user);
            loadUser();
        }

        public void updateUser(User user)
        {
            _userService.UpdateUser(user);
            loadUser();
        }

        public void deleteUser(User user)
        {
            _userService.DeleteUser(user);
            loadUser();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
