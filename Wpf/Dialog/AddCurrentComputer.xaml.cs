using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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
using Brushes = System.Windows.Media.Brushes;
using BusinessObjects;
using System.ComponentModel;
using Microsoft.VisualBasic.Devices;

namespace Wpf.Dialog
{
    /// <summary>
    /// Interaction logic for AddCurrentComputer.xaml
    /// </summary>
    public partial class AddCurrentComputer : Window, INotifyPropertyChanged
    {
        public CurrentComputer CComputer { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        
        private AcCommand submitCommand;

        public AcCommand SubmitCommand
        {
            get { return submitCommand; }
            set
            {
                submitCommand = value;
                OnPropertyChanged(nameof(SubmitCommand));
            }
        }

        public AddCurrentComputer(CurrentComputer currentComputer)
        {
            InitializeComponent();
            CComputer = currentComputer;
            SubmitCommand = new AcCommand(Submit, CanSubmit);
            DataContext = this;
            CComputer.PropertyChanged += CurrentComputer_PropertyChanged;
            CComputer.ErrorsChanged += CurrentComputer_ErrorsChanged;
        }

        private bool CanSubmit(object obj)
        {
            return (CComputer.UserId > 0 &&
                CComputer.ComputerId > 0 &&
                !CComputer.HasErrors);
        }

        private void Submit(object obj)
        {
            if (!CComputer.HasErrors)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please fix validation", MessageBox.MessageBoxTittle.Error, MessageBox.MessageBoxButton.Ok, MessageBox.MessageBoxButton.Confirm);
            }
        }
        private void CurrentComputer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }
        private void CurrentComputer_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void computerId_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenComputerId.Text = txtComputerId.Text;
            if (hiddenComputerId.Text.IsNullOrEmpty()) bdComputerId.Background = Brushes.Red;
            else bdComputerId.Background = Brushes.White;
        }

        private void userId_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenUserId.Text = txtUserId.Text;
            if (hiddenUserId.Text.IsNullOrEmpty()) bdUserId.Background = Brushes.Red;
            else bdUserId.Background = Brushes.White;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
