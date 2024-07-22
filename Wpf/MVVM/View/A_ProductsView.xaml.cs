using BusinessObjects;
using Service;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Dialog;
using Wpf.MVVM.ViewModel;
using Button = System.Windows.Controls.Button;
using UserControl = System.Windows.Controls.UserControl;

namespace Wpf.MVVM.View
{
    public partial class A_ProductsView : UserControl
    {
        private A_ProductsViewModel a_ProductsViewModel;
        public A_ProductsView()
        {
            InitializeComponent();
            a_ProductsViewModel = new A_ProductsViewModel(new ProductService());
            DataContext = a_ProductsViewModel;
            cboFilter.SelectedValue = 1;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Product newProduct = new Product();
            var createProduct = new CreateNewProduct(newProduct);
            if(createProduct.ShowDialog() == true)
            {
                a_ProductsViewModel.createProduct(newProduct);
                cboFilter.SelectedValue = 1;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Product product)
            {
                var currentProduct = product;
                var updateProduct = new UpdateProduct(currentProduct);
                if (updateProduct.ShowDialog() == true)
                {
                    a_ProductsViewModel.updateProduct(currentProduct);
                    cboFilter.SelectedValue = 1;
                }
                else
                {
                    a_ProductsViewModel.loadProduct();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            if (sender is Button button && button.DataContext is Product product)
            {
                DialogResult result = MessageBox.Show("Are you sure?", MessageBox.MessageBoxTittle.Confirm, MessageBox.MessageBoxButton.Confirm,
                                                   MessageBox.MessageBoxButton.Cancel);
                if (result == DialogResult.Yes)
                {
                    a_ProductsViewModel.deleteProduct(product);
                    cboFilter.SelectedValue = 1;
                }
            }
        }

        private void cboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Int32.Parse(cboFilter.SelectedValue.ToString()) == 1)
            {
                a_ProductsViewModel.loadProduct();
            }
            else if (Int32.Parse(cboFilter.SelectedValue.ToString()) == 2)
            {
               a_ProductsViewModel.loadFood();
            }
            else if (Int32.Parse(cboFilter.SelectedValue.ToString()) == 3)
            {
                a_ProductsViewModel.loadDrink();
            }
        }
    }
}
