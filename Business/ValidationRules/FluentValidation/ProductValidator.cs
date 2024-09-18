using Entities.Concretes;
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

        RuleFor(p => p.Name).Must(StartWithWithA).WithMessage("Ürünler A harfi ile başlamalı");
    }

    private bool StartWithWithA(string arg)
    {
        return arg.StartsWith("A");
    }
}
