using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.DTOs

{
    public class ProductDetailsDto
    {
       public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        
        public decimal Price { get; set; }
        public int Stock { get; set; } 
    }
}