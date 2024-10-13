using Core.Entities;
using System;

namespace Business.Dtos.Requests;

public class CategoryCreateDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
