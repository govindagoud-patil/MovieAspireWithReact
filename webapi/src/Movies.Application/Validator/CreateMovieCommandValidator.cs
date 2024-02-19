using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Movies.Application.Commands;
using Movies.Domain.Entities;

namespace Movies.Application.Validator
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator() {

            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage($"{nameof(Movie.Category)} cannot be empty")
                .MaximumLength(30)
                .WithMessage($"{nameof(Movie.Category)} cannot be longer than 30 characters");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage($"{nameof(Movie.Description)} cannot be empty")
                .MaximumLength(500)
                .WithMessage($"{nameof(Movie.Description)} cannot be longer than 500 characters");

            RuleFor(x => x.Title)
                 .NotEmpty()
                 .WithMessage($"{nameof(Movie.Title)} cannot be empty")
                 .MaximumLength(100)
                 .WithMessage($"{nameof(Movie.Title)} cannot be longer than 100 characters");

        }
    }
}
