using BusinessObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OrderDAO
    {
        public static ObservableCollection<Order> GetOrder()
        {
            var listOrder = new ObservableCollection<Order>();
            try
            {
                using var db = new PlayTechContext();
                var Order = db.Orders.ToList();
                listOrder = new ObservableCollection<Order>(Order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrder;
        }

        public static void SaveOrder(Order or)
        {
            try
            {
                using var context = new PlayTechContext();
                context.Orders.Add(or);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static void UpdateOrder(Order or)
        {
            try
            {
                using var context = new PlayTechContext();
                context.Entry(or).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrder(Order or)
        {
            try
            {
                using var context = new PlayTechContext();
                var p1 = context.Orders.SingleOrDefault(o => o.OrderId == or.OrderId);
                context.Orders.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrderByUserID(int userID)
        {
            try
            {
                using var context = new PlayTechContext();
                var orders = context.Orders.Where(o => o.UserId == userID).ToList();
                context.Orders.RemoveRange(orders);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrderByProductID(int productID)
        {
            try
            {
                using var context = new PlayTechContext();
                var orders = context.Orders.Where(o => o.ProductId == productID).ToList();
                context.Orders.RemoveRange(orders);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Order GetOrderById(int id)
        {
            using var db = new PlayTechContext();
            return db.Orders.FirstOrDefault(c => c.OrderId.Equals(id));
        }
    }
}
