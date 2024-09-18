using Core.Entities;
using Entities.Enums;
namespace Business.Dtos.Requests;

public class ProductCreateDto : IDto
{
    public string? Name { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
}
