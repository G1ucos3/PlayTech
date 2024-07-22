using BusinessObjects;
using DataAccessLayer;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IOrderRepository
    {
        void DeleteOrder(Order or);
        ObservableCollection<Order> GetOrder();
        Order GetOrderById(int id);
        void SaveOrder(Order or);
        void UpdateOrder(Order or);
        public void DeleteOrderByUserID(int userID);
        public void DeleteOrderByProductID(int productID);
    }
}