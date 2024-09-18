using System;
using FluentValidation;
using FluentValidation.Results;

namespace Core.CrossCuttingConcerns.Validation;

public static class ValidationTool
{
    public static void Validate<T>(IValidator<T> validator, T entity)
    {
        ValidationResult result = validator.Validate(entity);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}
