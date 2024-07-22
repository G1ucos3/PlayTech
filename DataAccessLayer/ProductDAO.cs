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
    public class ProductDAO
    {
        public static ObservableCollection<Product> GetProduct()
        {
            var listProduct = new ObservableCollection<Product>();
            try
            {
                using var db = new PlayTechContext();
                var product = db.Products.ToList();
                listProduct = new ObservableCollection<Product>(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProduct;
        }

        public static void SaveProduct(Product p)
        {
            try
            {
                using var context = new PlayTechContext();
                context.Products.Add(p);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static void UpdateProduct(Product p)
        {
            try
            {
                using var context = new PlayTechContext();
                context.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteProduct(Product p)
        {
            try
            {
                using var context = new PlayTechContext();
                var p1 = context.Products.SingleOrDefault(o => o.ProductId == p.ProductId);
                context.Products.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Product GetProductById(int id)
        {
            using var db = new PlayTechContext();
            return db.Products.FirstOrDefault(c => c.ProductId.Equals(id));
        }
    }
}
