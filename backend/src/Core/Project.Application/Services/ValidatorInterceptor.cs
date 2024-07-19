using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure.Exceptions;


namespace Project.Application.Services
{
    class ValidatorInterceptor : IValidatorInterceptor
    {
        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }
        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {

            if (!result.IsValid)
            {
                var errors = result.Errors.GroupBy(m => m.PropertyName)
                   .ToDictionary(m => m.Key, v => v.Select(m => m.ErrorCode));


                throw new BadRequestException("The data sent in the request does not meet the conditions.", errors);
            }

            return result;
        }
    }
}
