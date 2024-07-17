using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {

        public List<Order> GetOrder() => OrderDAO.GetOrder();

        public void SaveOrder(Order or) => OrderDAO.SaveOrder(or);

        public void UpdateOrder(Order or) => OrderDAO.UpdateOrder(or);

        public void DeleteOrder(Order or) => OrderDAO.DeleteOrder(or);

        public Order GetOrderById(int id) => OrderDAO.GetOrderById(id);
    }
}
