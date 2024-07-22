using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static MaterialDesignThemes.Wpf.Theme;
using Brushes = System.Windows.Media.Brushes;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window, INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        public User CurrentUser { get; set; }
        public User ConfirmUser { get; set; }
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
        public Register()
        {
            InitializeComponent();
            CurrentUser = new User();
            ConfirmUser = new User();
            _userService = new UserService();
            SubmitCommand = new AcCommand(Submit, CanSubmit);
            DataContext = this;
            CurrentUser.PropertyChanged += CurrentUser_PropertyChanged;
            CurrentUser.ErrorsChanged += CurrentUser_ErrorsChanged;
            ConfirmUser.PropertyChanged += CurrentUser_PropertyChanged;
            ConfirmUser.ErrorsChanged += CurrentUser_ErrorsChanged;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            this.Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUsername.Text;
            string email = txtEmail.Text;
            string userPassword = hiddenPasswordBox.Text;
            string confirmPass = hiddenConfirmPasswordBox.Text;
            if(_userService.GetUserByEmail(email) != null)
            {
                DialogResult result = MessageBox.Show("Email already exist! Login now?", MessageBox.MessageBoxTittle.Confirm, MessageBox.MessageBoxButton.Yes, MessageBox.MessageBoxButton.No);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    var login = new Login();
                    login.Show();
                    this.Close();
                }
            }
            else if(_userService.GetUserByUsername(userName) != null)
            {
                MessageBox.Show("Username already exist!", MessageBox.MessageBoxTittle.Error, MessageBox.MessageBoxButton.Confirm, MessageBox.MessageBoxButton.No);
            }
            else
            {
                if(userPassword.Equals(confirmPass))
                {
                    var currentUser = new User();
                    currentUser.UserName = userName;
                    currentUser.UserAvatar = "/Images/default.png";
                    currentUser.UserBalance = 0;
                    currentUser.UserPassword = userPassword;
                    currentUser.UserRoles = 3;
                    currentUser.UserEmail = email;
                    _userService.SaveUser(currentUser);
                    MessageBox.Show("User created successfully!", MessageBox.MessageBoxTittle.Info, MessageBox.MessageBoxButton.Confirm, MessageBox.MessageBoxButton.No);
                    var login = new Login();
                    login.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password not match!", MessageBox.MessageBoxTittle.Error, MessageBox.MessageBoxButton.Confirm, MessageBox.MessageBoxButton.No);
                }
            }
        }

        private bool CanSubmit(object obj)
        {
            return (!string.IsNullOrEmpty(CurrentUser.UserEmail) &&
                !string.IsNullOrEmpty(CurrentUser.UserName) &&
                !string.IsNullOrEmpty(CurrentUser.UserPassword) &&
                !CurrentUser.HasErrors);
        }

        private void Submit(object obj)
        {
            if (CurrentUser.HasErrors)
            {
                MessageBox.Show("Please fix validation", MessageBox.MessageBoxTittle.Info, MessageBox.MessageBoxButton.Ok, MessageBox.MessageBoxButton.Confirm);
            }
        }

        private void CurrentUser_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }
        private void CurrentUser_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void confirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            hiddenConfirmPasswordBox.Text = confirmPassword.Password;
            if (hiddenConfirmPasswordBox.Text.IsNullOrEmpty() || hiddenConfirmPasswordBox.Text.Length < 8) 
                bdConfirmPassword.Background = Brushes.Red;
            else bdConfirmPassword.Background = Brushes.White;
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            hiddenPasswordBox.Text = password.Password;
            if (hiddenPasswordBox.Text.IsNullOrEmpty() || hiddenPasswordBox.Text.Length < 8) 
                bdPassword.Background = Brushes.Red;
            else bdPassword.Background = Brushes.White;
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenEmail.Text = txtEmail.Text;
            if (hiddenEmail.Text.IsNullOrEmpty()) bdEmail.Background = Brushes.Red;
            else bdEmail.Background = Brushes.White;
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenUsername.Text = txtUsername.Text;
            if (hiddenUsername.Text.IsNullOrEmpty()) bdUsername.Background = Brushes.Red;
            else bdUsername.Background = Brushes.White;
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            btnHide.Visibility = Visibility.Collapsed;
            btnShow.Visibility = Visibility.Visible;
            password.Visibility = Visibility.Collapsed;
            showPasswordBox.Visibility = Visibility.Visible;
            showPasswordBox.Text = password.Password;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            btnHide.Visibility = Visibility.Visible;
            btnShow.Visibility = Visibility.Collapsed;
            password.Visibility = Visibility.Visible;
            showPasswordBox.Visibility = Visibility.Collapsed;
            password.Password = showPasswordBox.Text;
        }

        private void btnCPShow_Click(object sender, RoutedEventArgs e)
        {
            btnCPHide.Visibility = Visibility.Visible;
            btnCPShow.Visibility = Visibility.Collapsed;
            confirmPassword.Visibility = Visibility.Visible;
            showConfirmPasswordBox.Visibility = Visibility.Collapsed;
            confirmPassword.Password = showConfirmPasswordBox.Text;
        }

        private void btnCPHide_Click(object sender, RoutedEventArgs e)
        {
            btnCPHide.Visibility = Visibility.Collapsed;
            btnCPShow.Visibility = Visibility.Visible;
            confirmPassword.Visibility = Visibility.Collapsed;
            showConfirmPasswordBox.Visibility = Visibility.Visible;
            showConfirmPasswordBox.Text = confirmPassword.Password;
        }

        private void showPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenPasswordBox.Text = showPasswordBox.Text;
            if (hiddenPasswordBox.Text.IsNullOrEmpty() || hiddenPasswordBox.Text.Length < 8) 
                bdPassword.Background = Brushes.Red;
            else bdPassword.Background = Brushes.White;
        }

        private void showConfirmPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenConfirmPasswordBox.Text = showConfirmPasswordBox.Text;
            if (hiddenConfirmPasswordBox.Text.IsNullOrEmpty() || hiddenConfirmPasswordBox.Text.Length < 8) 
                bdConfirmPassword.Background = Brushes.Red;
            else bdConfirmPassword.Background = Brushes.White;
        }
    }
}
