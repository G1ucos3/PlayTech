using System;
using Wpf.Core;
using Wpf.MVVM.View;
using Service;

namespace Wpf.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand A_UsersCommand { get; set; }
        public RelayCommand A_ComputersCommand { get; set; }
        public RelayCommand A_ProductsCommand { get; set; }
        public RelayCommand M_UsersCommand { get; set; }
        public RelayCommand M_OrdersCommand { get; set; }

        public A_ComputersViewModel A_ComputersVM { get; set; }
        public A_ProductsViewModel A_ProductsVM { get; set; }
        public A_UsersViewModel A_UsersVM { get; set; }
        public M_UsersViewModel M_UsersVM { get; set; }
        public M_OrdersViewModel M_OrdersVM { get; set; }

        private object _currentView;
        private object m_currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged(); 
            }
        }

        public object M_CurrentView
        {
            get { return m_currentView; }
            set
            {
                m_currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            A_ComputersVM = new A_ComputersViewModel(new UserService(), new CurrentComputerService(), new ComputerService());
            A_ProductsVM = new A_ProductsViewModel(new ProductService());
            A_UsersVM = new A_UsersViewModel(new UserService(), new CurrentComputerService(), new ComputerService());
            CurrentView = A_UsersVM;

            M_UsersVM = new M_UsersViewModel();
            M_OrdersVM = new M_OrdersViewModel();
            M_CurrentView = M_UsersVM;

            A_UsersCommand = new RelayCommand(o =>
            {
                CurrentView = A_UsersVM;
            });

            A_ComputersCommand = new RelayCommand(o =>
            {
                CurrentView = A_ComputersVM;
            });

            A_ProductsCommand = new RelayCommand(o =>
            {
                CurrentView = A_ProductsVM;
            });

            M_UsersCommand = new RelayCommand(o =>
            {
                M_CurrentView = M_UsersVM;
            });

            M_OrdersCommand = new RelayCommand(o =>
            {
                M_CurrentView = M_OrdersVM;
            });
        }
    }
}
