using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApi.Models;
namespace ProductApi.Repository
{
    public interface IProductRepository
    {
         void Add(Product product);
        Product? GetById(int id);
        IEnumerable<Product> GetAll();
        void Update(Product product);
        void Delete(Product product);
        void Save();
    }
}