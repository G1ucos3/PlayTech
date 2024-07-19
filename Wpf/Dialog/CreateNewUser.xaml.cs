﻿using BusinessObjects;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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
using Wpf.MVVM.ViewModel;

namespace Wpf.Dialog
{
    /// <summary>
    /// Interaction logic for CreateNewUser.xaml
    /// </summary>
    public partial class CreateNewUser : Window, INotifyPropertyChanged
    {
        public User CurrentUser { get; set; }
        private AcCommand submitCommand;

        public CreateNewUser(User currentUser)
        {
            InitializeComponent();
            CurrentUser = currentUser;
            SubmitCommand = new AcCommand(Submit, CanSubmit); 
            DataContext = this;
            CurrentUser.PropertyChanged += CurrentUser_PropertyChanged;
            CurrentUser.ErrorsChanged += CurrentUser_ErrorsChanged;

            passwordBox.PasswordChanged += (s, e) =>
            {
                var passwordBox = s as PasswordBox;
                CurrentUser.UserPassword = passwordBox.Password;
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CurrentUser_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }
        private void CurrentUser_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
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

        private bool CanSubmit(object obj)
        {
            return (!string.IsNullOrEmpty(CurrentUser.UserName) && 
                !string.IsNullOrEmpty(CurrentUser.UserPassword) &&
                !string.IsNullOrEmpty(CurrentUser.UserEmail) &&
                CurrentUser.UserRoles > 1 && CurrentUser.UserRoles < 4 &&
                !CurrentUser.HasErrors);
        }

        private void Submit(object obj)
        {
            if (!CurrentUser.HasErrors)
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

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                hiddenPasswordBox.Text = passwordBox.Password;
            }
        }
    }
}