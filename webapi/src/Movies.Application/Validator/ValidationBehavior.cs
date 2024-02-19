using FluentValidation;
using MediatR;
using Movies.Contracts;
using Movies.Contracts.Errors;
using Movies.Contracts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Validator
{

    /// <summary>
    /// configure the validation behavior or handle validation in the pipeline middleware
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Handles the validation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="next"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="CustomValidationException"></exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext<TRequest>(request);
            var validationResult = await Task.WhenAll(_validators.Select(x=> x.ValidateAsync(validationContext,cancellationToken)));

            var validationFailure = validationResult.Where(x => !x.IsValid)
                                                    .SelectMany(x => x.Errors)
                                                    .Select(x=> new ValidationError() { Property=x.PropertyName,ErrorMessage=x.ErrorMessage }).ToList();
            if(validationFailure.Any())
            {
                throw new CustomValidationException(validationFailure);
            }

          return await next.Invoke();
        }
    }
}
