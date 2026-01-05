using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApi.Models;
using ProductApi.Data;
using ProductApi.DTOs;
using AutoMapper;
using ProductApi.Exceptions;
using ProductApi.Repository;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        
           private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
         public ProductService(IProductRepository repository,IMapper mapper)
        {
           _repository = repository;
            _mapper = mapper;
        }       
         public void Add(AddProductDto dto)
        {
           var product = _mapper.Map<Product>(dto);//create new instance of Product model and copy matching values from dto into it. 
            // {
            //     Name = dto.Name,
            //     Description = dto.Description,
            //     Price = dto.Price,
            //     Stock = dto.Stock,
            //     CreatedAt = DateTime.Now
            // };

            _repository.Add(product);
             _repository.Save();
        }

        public void Delete(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with id {id} not found.");
            }

             _repository.Delete(product);
            _repository.Save();
        }

        public IEnumerable<ProductDetailsDto> GetAll()
        {

            var products = _repository.GetAll();
            return _mapper.Map<IEnumerable<ProductDetailsDto>>(products);
            //
        }

        public ProductDetailsDto? GetById(int Id)
        {

         var product =  _repository.GetById(Id);
            //if (product == null) return null;
            if (product == null)
            {
                throw new NotFoundException($"Product with id {Id} not found.");
            }

            return _mapper.Map<ProductDetailsDto>(product);
              
        }

        public void Update(int id, AddProductDto dto)
        {
           var product =  _repository.GetById(id);;
           // if (product == null) return;
           if(product == null)
            {
                throw new NotFoundException($"Product with id {id} not found.");
            }
            _mapper.Map(dto,product);
             _repository.Update(product);
            _repository.Save();
        }
    }
}