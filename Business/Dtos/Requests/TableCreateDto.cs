using Core.Entities;
using Entities.Enums;

namespace Business.Dtos.Requests;

public class TableCreateDto : IDto
{
    public Guid Id { get; set; }
    public string TableName { get; set; }
    public TableStatus Status { get; set; }
}
