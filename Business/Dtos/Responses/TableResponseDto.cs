using System;
using Core.Entities;
using Entities.Enums;

namespace Business.Dtos.Responses;

public class TableResponseDto : IDto
{
    public Guid Id { get; set; }
    public string TableName { get; set; }
    public TableStatus Status { get; set; }
}
