using BusinessObjects;
using DataAccessLayer;
using Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository iProductRepository;

        public ProductService()
        {
            iProductRepository = new ProductRepository();
        }

        public ObservableCollection<Product> GetProduct() => iProductRepository.GetProduct();

        public void SaveProduct(Product p) => iProductRepository.SaveProduct(p);

        public void UpdateProduct(Product p) => iProductRepository.UpdateProduct(p);

        public void DeleteProduct(Product p) => iProductRepository.DeleteProduct(p);

        public Product GetProductById(int id) => iProductRepository.GetProductById(id);

    }
}
