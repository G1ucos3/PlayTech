using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Path = System.IO.Path;

namespace Wpf.Dialog
{

    public class IntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //convert the int to a string:
            if (value == null) return 0;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //convert the string back to an int here
            if(value.ToString().IsNullOrEmpty()) return 0;
            int n;
            bool isNumeric = int.TryParse(value.ToString(), out n);
            if(!isNumeric ) return 0;
            return int.Parse(value.ToString());
        }
    }

    /// <summary>
    /// Interaction logic for CreateNewProduct.xaml
    /// </summary>
    public partial class CreateNewProduct : Window, INotifyPropertyChanged
    {
        public Product CurrentProduct { get; set; }
        private AcCommand submitCommand;

        public CreateNewProduct(Product currentProduct)
        {
            InitializeComponent();
            CurrentProduct = currentProduct;
            SubmitCommand = new AcCommand(Submit, CanSubmit);
            DataContext = this;
            CurrentProduct.PropertyChanged += CurrentProduct_PropertyChanged;
            CurrentProduct.ErrorsChanged += CurrentProduct_ErrorsChanged;
        }

        private void CurrentProduct_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }
        private void CurrentProduct_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
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
            return (!string.IsNullOrEmpty(CurrentProduct.ProductName) &&
                CurrentProduct.ProductPrice > 0 &&
                CurrentProduct.ProductQuantity > 0 &&
                !string.IsNullOrEmpty(CurrentProduct.ProductImg) &&
                !CurrentProduct.HasErrors);
        }

        private void Submit(object obj)
        {
            if (!CurrentProduct.HasErrors)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please fix validation", MessageBox.MessageBoxTittle.Info, MessageBox.MessageBoxButton.Ok, MessageBox.MessageBoxButton.Confirm);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                productImage.ImageSource = new BitmapImage(new Uri(projectDirectory + CurrentProduct.ProductImg, UriKind.Absolute));
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
                string targetFolder = Path.Combine(projectDirectory, "Images", "product");

                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                // Define the full path where the image will be saved
                string targetPath = Path.Combine(targetFolder, fileName);

                // Copy the image file to the target path
                File.Copy(filePath, targetPath, true);

                // Generate the relative path to save in the database
                string relativePath = $"/Images/product/{fileName}";
                CurrentProduct.ProductImg = relativePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image error", MessageBox.MessageBoxTittle.Error, MessageBox.MessageBoxButton.Ok, MessageBox.MessageBoxButton.Confirm);
            }
        }

        private void productname_TextChanged(object sender, TextChangedEventArgs e)
        {
            hiddenProductName.Text = productName.Text;
            if (hiddenProductName.Text.IsNullOrEmpty()) bdName.Background = Brushes.Red;
            else bdName.Background = Brushes.White;
        }

        private void quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (quantity.Text.IsNullOrEmpty()) quantity.Text = "0";
            int n;
            bool isNumeric = int.TryParse(quantity.Text, out n);
            if (!isNumeric) quantity.Text = "0";
            hiddenQuantity.Text = quantity.Text;
            if(hiddenQuantity.Text.IsNullOrEmpty()) bdQuantity.Background = Brushes.Red;
            else bdQuantity.Background = Brushes.White;
            if (isNumeric && n <= 0) bdQuantity.Background = Brushes.Red;
            else bdQuantity.Background = Brushes.White;
        }

        private void price_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (price.Text.IsNullOrEmpty()) price.Text = "0";
            int n;
            bool isNumeric = int.TryParse(price.Text, out n);
            if (!isNumeric) price.Text = "0";
            hiddenPrice.Text = price.Text;
            if (hiddenPrice.Text.IsNullOrEmpty()) bdPrice.Background = Brushes.Red;
            else bdPrice.Background = Brushes.White;
            if (isNumeric && n <= 0) bdPrice.Background = Brushes.Red;
            else bdPrice.Background = Brushes.White;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
