using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel;

namespace Wpf.MVVM.ViewModel
{
    class M_OrdersViewModel : INotifyPropertyChanged
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private ObservableCollection<Order> orders;

        public ObservableCollection<Order> Orders
        {
            get => orders;
            set
            {
                if (orders != value)
                {
                    orders = value;
                    OnPropertyChanged(nameof(Orders));
                }
            }
        }

        public M_OrdersViewModel(IUserService userService, IOrderService orderService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
            _orderService = orderService;
            loadOrder();
        }

        public void loadOrder()
        {
            var listOrder = _orderService.GetOrder();
            var list = new ObservableCollection<Order>();
            foreach (var item in listOrder)
            {
                item.User = _userService.GetUserById(item.UserId);
                item.Product = _productService.GetProductById(item.ProductId);
                list.Add(item);
            }
            Orders = list;
        }

        public void loadPurchased()
        {
            var listPurchased = _orderService.GetOrder();
            var list = new ObservableCollection<Order>();
            foreach(var item in listPurchased)
            {
                if (item.OrderStatus == false) continue;
                item.User = _userService.GetUserById(item.UserId);
                item.Product = _productService.GetProductById(item.ProductId);
                list.Add(item);
            }
            Orders = list;
        }

        public void loadUnpurchased()
        {
            var listUnpurchased = _orderService.GetOrder();
            var list = new ObservableCollection<Order>();
            foreach (var item in listUnpurchased)
            {
                if (item.OrderStatus == true) continue;
                item.User = _userService.GetUserById(item.UserId);
                item.Product = _productService.GetProductById(item.ProductId);
                list.Add(item);
            }
            Orders = list;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void deleteOrder(Order order)
        {
            _orderService.DeleteOrder(order);
            loadOrder();
        }

        public void updateOrder(Order order)
        {
            _orderService.UpdateOrder(order);
            loadOrder();
        }

    }
}
