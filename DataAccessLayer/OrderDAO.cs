using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OrderDAO
    {
        public static List<Order> GetOrder()
        {
            var listOrder = new List<Order>();
            try
            {
                using var db = new PlayTechContext();
                listOrder = db.Orders.ToList();
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
                var p1 = context.Orders.SingleOrDefault(o => o.UserId == or.UserId);
                context.Orders.Remove(p1);
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
