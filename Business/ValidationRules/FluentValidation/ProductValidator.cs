using Entities.Concretes;
using Entities.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation;

public class ProductValidator:AbstractValidator<Product>
{
    public ProductValidator()
    {
       RuleFor(p=>p.Name).NotEmpty();

        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");
        RuleFor(p => p.Name).NotEmpty().WithMessage("Ürün adı boş geçilemez.");
        RuleFor(p => p.Status).IsInEnum().WithMessage("Ürün statusu bo ş olamaz");
    }

  
}
