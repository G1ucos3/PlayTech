using BusinessObjects;

namespace Service
{
    public interface IOrderService
    {
        void DeleteOrder(Order or);
        List<Order> GetOrder();
        Order GetOrderById(int id);
        void SaveOrder(Order or);
        void UpdateOrder(Order or);
    }
}