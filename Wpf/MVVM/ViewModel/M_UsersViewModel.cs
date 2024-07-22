using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace Wpf.MVVM.ViewModel
{
    class M_UsersViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private readonly ICurrentComputerService _currentComputerService;
        private readonly IComputerService _computerService;
        private ObservableCollection<User> users;

        public ObservableCollection<User> Users
        {
            get => users;
            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        public M_UsersViewModel(IUserService userService, ICurrentComputerService currentComputerService, IComputerService computerService)
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
