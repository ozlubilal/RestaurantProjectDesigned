using Core.Entities;
using System;

namespace Business.Dtos.Responses;

public class CategoryResponseDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
