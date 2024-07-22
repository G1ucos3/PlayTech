using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Wpf.MVVM.View;
using System.IO;
using Path = System.IO.Path;
using System.Windows.Forms;

namespace Wpf.Dialog
{
    /// <summary>
    /// Interaction logic for EditProfile.xaml
    /// </summary>
    public partial class EditProfile : Window, INotifyPropertyChanged
    {
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
        public EditProfile(User currentUser)
        {
            InitializeComponent();
            CurrentUser = currentUser;
            SubmitCommand = new AcCommand(Submit, CanSubmit);
            DataContext = this;
            CurrentUser.PropertyChanged += CurrentUser_PropertyChanged;
            CurrentUser.ErrorsChanged += CurrentUser_ErrorsChanged;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = hiddenPasswordBox.Text;
            txtUsername.Text = hiddenUsername.Text;
            email.Text = hiddenEmail.Text;
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            userAvatar.ImageSource = new BitmapImage(new Uri(projectDirectory + CurrentUser.UserAvatar, UriKind.Absolute));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool CanSubmit(object obj)
        {
            return (!string.IsNullOrEmpty(CurrentUser.UserName) &&
                !string.IsNullOrEmpty(CurrentUser.UserPassword) &&
                !string.IsNullOrEmpty(CurrentUser.UserEmail) &&
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
                MessageBox.Show("Please fix validation", MessageBox.MessageBoxTittle.Error, MessageBox.MessageBoxButton.Ok, MessageBox.MessageBoxButton.Confirm);
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

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenEmail.Text = email.Text;
            if (email.Text.IsNullOrEmpty()) bdEmail.Background = Brushes.Red;
            else bdEmail.Background = Brushes.White;
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenUsername.Text = txtUsername.Text;
            if (hiddenUsername.Text.IsNullOrEmpty()) bdUsername.Background = Brushes.Red;
            else bdUsername.Background = Brushes.White;
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

        private void passwordBox_PasswordChanged_1(object sender, RoutedEventArgs e)
        {
            hiddenPasswordBox.Text = passwordBox.Password;
            if (hiddenPasswordBox.Text.IsNullOrEmpty() || hiddenPasswordBox.Text.Length < 8) bdPassword.Background = Brushes.Red;
            else bdPassword.Background = Brushes.White;
        }

        private void showPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenPasswordBox.Text = showPasswordBox.Text;
            if (hiddenPasswordBox.Text.IsNullOrEmpty() || hiddenPasswordBox.Text.Length < 8) bdPassword.Background = Brushes.Red;
            else bdPassword.Background = Brushes.White;
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg, *.png) | *.jpg; *.png";
            if (ofd.ShowDialog() == true)
            {
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                saveImage(ofd.FileName);
                userAvatar.ImageSource = new BitmapImage(new Uri(projectDirectory + CurrentUser.UserAvatar, UriKind.Absolute));
            }
        }

        private void saveImage(string filePath) 
        {
            try
            {
                string fileExtension = Path.GetExtension(filePath);
                string fileName = $"{DateTime.Now.Ticks}{fileExtension}";
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                string targetFolder = Path.Combine(projectDirectory, "Images", "avatar");

                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                // Define the full path where the image will be saved
                string targetPath = Path.Combine(targetFolder, fileName);

                // Copy the image file to the target path
                File.Copy(filePath, targetPath, true);

                // Generate the relative path to save in the database
                string relativePath = $"/Images/avatar/{fileName}";
                CurrentUser.UserAvatar = relativePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image error", MessageBox.MessageBoxTittle.Error, MessageBox.MessageBoxButton.Ok, MessageBox.MessageBoxButton.Confirm);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
