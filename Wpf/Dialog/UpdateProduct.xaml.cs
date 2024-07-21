using BusinessObjects;
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

namespace Wpf.Dialog
{
    /// <summary>
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Window, INotifyPropertyChanged
    {
        public Product CurrentProduct { get; set; }
        private AcCommand submitCommand;

        public UpdateProduct(Product currentProduct)
        {
            InitializeComponent();
            CurrentProduct = currentProduct;
            SubmitCommand = new AcCommand(Submit, CanSubmit);
            DataContext = this;
            CurrentProduct.PropertyChanged += CurrentProduct_PropertyChanged;
            CurrentProduct.ErrorsChanged += CurrentProduct_ErrorsChanged;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbType.SelectedValue = CurrentProduct.ProductType;
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
                !string.IsNullOrEmpty(CurrentProduct.ProductPrice.ToString()) &&
                !string.IsNullOrEmpty(CurrentProduct.ProductQuantity.ToString()) &&
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
