using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    }
}
