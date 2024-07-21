using BusinessObjects;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.MVVM.View;

namespace Wpf.Dialog
{
    public partial class UpdateComputer : Window, INotifyPropertyChanged
    {
        public Computer CurrentComputer { get; set; }
        private AcCommand submitCommand;
        private readonly IUserService _userService;

        public UpdateComputer(Computer currentComputer)
        {
            InitializeComponent();
            CurrentComputer = currentComputer;
            SubmitCommand = new AcCommand(Submit, CanSubmit);
            DataContext = this;
            _userService = new UserService();
            CurrentComputer.PropertyChanged += CurrentComputer_PropertyChanged;
            CurrentComputer.ErrorsChanged += CurrentComputer_ErrorsChanged;
        }

        public AcCommand SubmitCommand
        {
            get { return submitCommand; }
            set
            {
                submitCommand = value;
                OnPropertyChanged(nameof(SubmitCommand));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CurrentComputer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }
        private void CurrentComputer_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }

        private bool CanSubmit(object obj)
        {
            return (!string.IsNullOrEmpty(CurrentComputer.ComputerName) &&
                !string.IsNullOrEmpty(CurrentComputer.ComputerSpec) &&
                !CurrentComputer.HasErrors);
        }

        private void Submit(object obj)
        {
            if (!CurrentComputer.HasErrors)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please fix validation", MessageBox.MessageBoxTittle.Info, MessageBox.MessageBoxButton.Ok, MessageBox.MessageBoxButton.Confirm);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbStatus.SelectedValue = CurrentComputer.ComputerStatus;
            Console.WriteLine(CurrentComputer.ComputerStatus);
        }
    }
}
