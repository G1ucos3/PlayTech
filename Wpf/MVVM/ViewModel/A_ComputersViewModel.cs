using BusinessObjects;
using Microsoft.VisualBasic.ApplicationServices;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.MVVM.View;

namespace Wpf.MVVM.ViewModel
{
    class A_ComputersViewModel: INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private readonly ICurrentComputerService _currentComputerService;
        private readonly IComputerService _computerService;
        private ObservableCollection<Computer> computers;

        public ObservableCollection<Computer> Computers
        {
            get => computers;
            set
            {
                computers = value;
                OnPropertyChanged(nameof(Computers));
            }
        }

        public A_ComputersViewModel() { }

        public A_ComputersViewModel(IUserService userService, ICurrentComputerService currentComputerService, IComputerService computerService)
        {
            _userService = userService;
            _currentComputerService = currentComputerService;
            _computerService = computerService;
            loadComputer();
        }

        public void loadComputer()
        {
            var list = new ObservableCollection<Computer>();
            var listComputer = _computerService.GetComputer();
            foreach (var computer in listComputer)
            {
                Console.WriteLine(computer.ComputerName);
                computer.CurrentComputers = _currentComputerService.GetCurrentComputerByComputerID(computer.ComputerId).ToList();
                foreach(var user in computer.CurrentComputers)
                {
                    user.User = _userService.GetUserById(user.UserId);
                }
                list.Add(computer);
            }
            Computers = list;
        }

        public void loadOnlineComputer()
        {
            var list = new ObservableCollection<Computer>();
            var listComputer = _computerService.GetComputer();
            foreach (var computer in listComputer)
            {
                if (computer.ComputerStatus) continue;
                computer.CurrentComputers = _currentComputerService.GetCurrentComputerByComputerID(computer.ComputerId).ToList();
                foreach (var user in computer.CurrentComputers)
                {
                    user.User = _userService.GetUserById(user.UserId);
                }
                list.Add(computer);
            }
            Computers = list;
        }

        public void loadOfflineComputer()
        {
            var list = new ObservableCollection<Computer>();
            var listComputer = _computerService.GetComputer();
            foreach (var computer in listComputer)
            {
                if (!computer.ComputerStatus) continue;
                computer.CurrentComputers = _currentComputerService.GetCurrentComputerByComputerID(computer.ComputerId).ToList();
                foreach (var user in computer.CurrentComputers)
                {
                    user.User = _userService.GetUserById(user.UserId);
                }
                list.Add(computer);
            }
            Computers = list;
        }

        public void createComputer(Computer computer)
        {
            _computerService.SaveComputer(computer);
            loadComputer();
        }

        public void updateComputer(Computer computer)
        {
            _computerService.UpdateComputer(computer);
            loadComputer();
        }

        public void deleteComputer(Computer computer)
        {
            _computerService.DeleteComputer(computer);
            loadComputer();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
