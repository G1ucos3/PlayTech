using BusinessObjects;
using System.Collections.ObjectModel;

namespace Repositories
{
    public interface IProductRepository
    {
        void DeleteProduct(Product p);
        ObservableCollection<Product> GetProduct();
        Product GetProductById(int id);
        void SaveProduct(Product p);
        void UpdateProduct(Product p);
    }
}