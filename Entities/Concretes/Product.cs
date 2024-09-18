using Core.Entities;
using Entities.Concretes;
using Entities.Enums;

public class Product : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Price { get; set; }

    // Enum özelliğini ekliyoruz
    public ProductStatus Status { get; set; } 

    // Navigation Property
    public Category? Category { get; set; }
}