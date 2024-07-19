using BusinessObjects;
using System.Collections.ObjectModel;

namespace Service
{
    public interface IOrderService
    {
        void DeleteOrder(Order or);
        ObservableCollection<Order> GetOrder();
        Order GetOrderById(int id);
        void SaveOrder(Order or);
        void UpdateOrder(Order or);
    }
}