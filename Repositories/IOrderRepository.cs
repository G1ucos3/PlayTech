using BusinessObjects;

namespace Repositories
{
    public interface IOrderRepository
    {
        void DeleteOrder(Order or);
        List<Order> GetOrder();
        Order GetOrderById(int id);
        void SaveOrder(Order or);
        void UpdateOrder(Order or);
    }
}