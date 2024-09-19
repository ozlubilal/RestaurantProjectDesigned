using System;
using Core.Entities;
using Entities.Enums;

namespace Business.Dtos.Requests;

public class StoreBillUpdateDto : IDto
{
    public Guid Id { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime ClosedDate { get; set; }
    public StoreBillStatus Status { get; set; }
}
