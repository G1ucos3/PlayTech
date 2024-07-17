using BusinessObjects;

namespace Repositories
{
    public interface IProductRepository
    {
        void DeleteProduct(Product p);
        List<Product> GetProduct();
        Product GetProductById(int id);
        void SaveProduct(Product p);
        void UpdateProduct(Product p);
    }
}