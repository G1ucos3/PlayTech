using BusinessObjects;
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
    }
}