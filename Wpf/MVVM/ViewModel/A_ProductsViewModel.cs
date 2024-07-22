using BusinessObjects;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.MVVM.ViewModel
{
    class A_ProductsViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;
        private ObservableCollection<Product> products;
        private readonly IOrderService _orderService;

        public ObservableCollection<Product> Products
        {
            get => products;
            set
            {
                if(products != value)
                {
                    products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }

        public A_ProductsViewModel() { }

        public A_ProductsViewModel(IProductService productService)
        {
            _productService = productService;
            _orderService = new OrderService();
            loadProduct();
        }

        public void loadProduct()
        {
            var listProduct = _productService.GetProduct();
            var list = new ObservableCollection<Product>(listProduct);
            Products = list;
        }

        public void loadFood()
        {
            var listProduct = _productService.GetProduct().Where(p => p.ProductType.Equals("Food"));
            var list = new ObservableCollection<Product>(listProduct);
            Products = list;
        }

        public void loadDrink()
        {
            var listProduct = _productService.GetProduct().Where(p => p.ProductType.Equals("Drink"));
            var list = new ObservableCollection<Product>(listProduct);
            Products = list;
        }

        public void createProduct(Product product)
        {
            _productService.SaveProduct(product);
            loadProduct();
        }

        public void updateProduct(Product product)
        {
            _productService.UpdateProduct(product);
            loadProduct();
        }

        public void deleteProduct(Product product)
        {
            _orderService.DeleteOrderByProductID(product.ProductId);
            _productService.DeleteProduct(product);
            loadProduct();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
