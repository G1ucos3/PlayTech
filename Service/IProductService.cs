using BusinessObjects;

namespace Service
{
    public interface IProductService
    {
        void DeleteProduct(Product p);
        List<Product> GetProduct();
        Product GetProductById(int id);
        void SaveProduct(Product p);
        void UpdateProduct(Product p);
    }
}