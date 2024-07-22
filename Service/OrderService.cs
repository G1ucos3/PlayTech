using BusinessObjects;
using DataAccessLayer;
using Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository iOrderRepository;

        public OrderService()
        {
            iOrderRepository = new OrderRepository();
        }

        public ObservableCollection<Order> GetOrder() => iOrderRepository.GetOrder();

        public void SaveOrder(Order or) => iOrderRepository.SaveOrder(or);

        public void UpdateOrder(Order or) => iOrderRepository.UpdateOrder(or);

        public void DeleteOrder(Order or) => iOrderRepository.DeleteOrder(or);

        public Order GetOrderById(int id) => iOrderRepository.GetOrderById(id);

        public void DeleteOrderByUserID(int userID) => iOrderRepository.DeleteOrderByUserID(userID);

        public void DeleteOrderByProductID(int productID) => iOrderRepository.DeleteOrderByProductID(productID);
    }
}
