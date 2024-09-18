using System;
using Core.Entities;
using Entities.Enums;

namespace Business.Dtos.Requests;

public class StoreBillCreateDto : IDto
{
    public decimal TotalAmount { get; set; }
    public StoreBillStatus Status { get; set; }
}
