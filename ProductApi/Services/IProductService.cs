using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApi.DTOs;

namespace ProductApi.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDetailsDto> GetAll();
        ProductDetailsDto? GetById(int Id);
        void Add(AddProductDto dto);
        void Update(int id, AddProductDto dto);
        void Delete(int id);

    }
}