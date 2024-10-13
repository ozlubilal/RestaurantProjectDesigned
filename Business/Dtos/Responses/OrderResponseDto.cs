using Core.Entities;
using Entities.Enums;
using System;

namespace Business.Dtos.Responses;

public class OrderResponseDto : IDto
{
    public Guid Id { get; set; }
    public Guid BillId { get; set; }
    public Guid ProductId { get; set; }
    public Guid TableId { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string ProductName { get; set; }
    public DateTime BillDate { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public OrderStatus Status { get; set; }
    public string TableName { get; set; }
}
