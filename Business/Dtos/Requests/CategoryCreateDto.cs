using Core.Entities;
using System;

namespace Business.Dtos.Requests;

public class CategoryCreateDto : IDto
{
    public string Name { get; set; }
}
