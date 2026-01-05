using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApi.Models;
using ProductApi.Data;
using ProductApi.DTOs;
using AutoMapper;
using ProductApi.Exceptions;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
         public ProductService(AppDbContext context,IMapper mapper)
        {
            _context = context;
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

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with id {id} not found.");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IEnumerable<ProductDetailsDto> GetAll()
        {

            var products =  _context.Products.ToList();
            return _mapper.Map<IEnumerable<ProductDetailsDto>>(products);
            //
        }

        public ProductDetailsDto? GetById(int Id)
        {

         var product = _context.Products.Find(Id);
            //if (product == null) return null;
            if (product == null)
            {
                throw new NotFoundException($"Product with id {Id} not found.");
            }

            return _mapper.Map<ProductDetailsDto>(product);
              
        }

        public void Update(int id, AddProductDto dto)
        {
           var product = _context.Products.Find(id);
           // if (product == null) return;
           if(product == null)
            {
                throw new NotFoundException($"Product with id {id} not found.");
            }
            _mapper.Map(dto,product);
            _context.SaveChanges();
        }
    }
}