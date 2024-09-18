using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;
using System;
using System.Linq;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;
        private readonly Type _entityType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
            _entityType = _validatorType.BaseType.GetGenericArguments()[0];
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entities = invocation.Arguments.Where(arg => arg != null && arg.GetType() == _entityType);

            foreach (var entity in entities)
            {
                // Create a generic method to call Validate on the validator
                var validateMethod = typeof(ValidationTool).GetMethod(nameof(ValidationTool.Validate))
                    .MakeGenericMethod(_entityType);

                // Call the Validate method with the validator and entity
                validateMethod.Invoke(null, new[] { validator, entity });
            }
        }
    }
}
