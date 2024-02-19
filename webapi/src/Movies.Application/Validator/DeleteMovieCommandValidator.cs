using FluentValidation;
using Movies.Application.Commands;
using Movies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Validator
{
    public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage($"{ nameof(Movie.Id)} cannot be null");
                
        }
    }
}
