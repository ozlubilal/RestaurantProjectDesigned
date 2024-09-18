using Core.Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Responses
{
    public class ProductResponseDto : IDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public string? CategoryName { get; set; } // İlgili kategori adı
    }

}
