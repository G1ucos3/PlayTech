using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Service;
using System;
using System.IO;
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
using Brushes = System.Windows.Media.Brushes;
using TextBox = System.Windows.Controls.TextBox;
using Path = System.IO.Path;
using Wpf.MVVM.View;

namespace Wpf
{
        public partial class Login : Window, INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        public User CurrentUser { get; set; }
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

        public Login()
        {
            InitializeComponent();
            CurrentUser = new User();
            _userService = new UserService();
            SubmitCommand = new AcCommand(Submit, CanSubmit);
            DataContext = this;
            CurrentUser.PropertyChanged += CurrentUser_PropertyChanged;
            CurrentUser.ErrorsChanged += CurrentUser_ErrorsChanged;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userEmail = email.Text;
            string userPassword = hiddenPasswordBox.Text;
            var currentUser = _userService.GetUserByEmail(userEmail);
            if (currentUser != null)
            {
                if (userPassword.Equals(currentUser.UserPassword))
                {
                    if(currentUser.UserRoles == 1)
                    {
                        MessageBox.Show("Welcome back Admin!", MessageBox.MessageBoxTittle.Info, MessageBox.MessageBoxButton.Confirm, MessageBox.MessageBoxButton.No);
                        var admin = new Admin();
                        admin.txtUserID.Text = currentUser.UserId.ToString();
                        admin.txtUsername.Text = currentUser.UserName;
                        var converter = new ImageSourceConverter();
                        //MessageBox.Show(currentUser.UserAvatar, MessageBox.MessageBoxTittle.Info, MessageBox.MessageBoxButton.Confirm, MessageBox.MessageBoxButton.No);
                        //admin.avatar.ImageSource = (ImageSource)converter.ConvertFromString("pack://application:,,," + currentUser.UserAvatar);
                        string workingDirectory = Environment.CurrentDirectory;
                        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                        admin.avatar.ImageSource = new BitmapImage(new Uri(projectDirectory + currentUser.UserAvatar, UriKind.Absolute));
                        admin.Show();
                        this.Close();
                    }
                    else if(currentUser.UserRoles == 2)
                    {
                        MessageBox.Show($"Welcome back {currentUser.UserName}", MessageBox.MessageBoxTittle.Info, MessageBox.MessageBoxButton.Confirm, MessageBox.MessageBoxButton.No);
                        var manager = new Manager();
                        manager.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"Welcome back {currentUser.UserName}", MessageBox.MessageBoxTittle.Info, MessageBox.MessageBoxButton.Confirm, MessageBox.MessageBoxButton.No);
                    }
                }
                else
                {
                    MessageBox.Show("Password is incorrect", MessageBox.MessageBoxTittle.Error, MessageBox.MessageBoxButton.Confirm, MessageBox.MessageBoxButton.No);
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Email not exist, register now?", MessageBox.MessageBoxTittle.Confirm, MessageBox.MessageBoxButton.Yes, MessageBox.MessageBoxButton.No);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    var register = new Register();
                    register.Show();
                    this.Close();
                }
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var register = new Register();
            register.Show();
            this.Close();
        }

        private bool CanSubmit(object obj)
        {
            return (!string.IsNullOrEmpty(CurrentUser.UserEmail) &&
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

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            hiddenPasswordBox.Text = passwordBox.Password;
            if (hiddenPasswordBox.Text.IsNullOrEmpty() || hiddenPasswordBox.Text.Length < 8) bdPassword.Background = Brushes.Red;
            else bdPassword.Background = Brushes.White;
        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenText.Text = email.Text;
            if (email.Text.IsNullOrEmpty()) bdEmail.Background = Brushes.Red;
            else bdEmail.Background = Brushes.White;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            btnHide.Visibility = Visibility.Visible;
            btnShow.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;
            showPasswordBox.Visibility = Visibility.Collapsed;
            passwordBox.Password = showPasswordBox.Text;
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            btnHide.Visibility = Visibility.Collapsed;
            btnShow.Visibility = Visibility.Visible;
            passwordBox.Visibility = Visibility.Collapsed;
            showPasswordBox.Visibility = Visibility.Visible;
            showPasswordBox.Text = passwordBox.Password;
        }

        private void showPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenPasswordBox.Text = showPasswordBox.Text;
            if (hiddenPasswordBox.Text.IsNullOrEmpty() || hiddenPasswordBox.Text.Length < 8) bdPassword.Background = Brushes.Red;
            else bdPassword.Background = Brushes.White;
        }
    }
}
