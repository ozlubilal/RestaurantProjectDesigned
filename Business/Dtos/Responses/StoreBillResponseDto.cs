using System;
using Core.Entities;
using Entities.Enums;

namespace Business.Dtos.Responses;

public class StoreBillResponseDto : IDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public decimal TotalAmount { get; set; }
    public StoreBillStatus Status { get; set; }
}
