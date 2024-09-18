using Core.Entities;
using Entities.Enums;
using System;

namespace Business.Dtos.Requests;

public class OrderCreateDto : IDto
{
    public Guid ProductId { get; set; }
    public Guid BillId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Guid UserId { get; set; }
    public OrderStatus Status { get; set; }
}
