using System;
using Wpf.Core;
using Wpf.MVVM.View;

namespace Wpf.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand A_UsersCommand { get; set; }
        public RelayCommand A_ComputersCommand { get; set; }
        public RelayCommand A_ProductsCommand { get; set; }

        public A_ComputersViewModel A_ComputersVM { get; set; }
        public A_ProductsViewModel A_ProductsVM { get; set; }
        public A_UsersViewModel A_UsersVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged(); 
            }
        }


        public MainViewModel()
        {
            A_ComputersVM = new A_ComputersViewModel();
            A_ProductsVM = new A_ProductsViewModel();
            A_UsersVM = new A_UsersViewModel();
            CurrentView = A_UsersVM;

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
        }
    }
}
