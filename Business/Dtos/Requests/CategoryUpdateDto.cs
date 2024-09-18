using Core.Entities;
using System;

namespace Business.Dtos.Requests;

public class CategoryUpdateDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
