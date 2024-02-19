using MediatR;
using Movies.Infrastructure;

using Movies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Movies.Contracts;
using Movies.Contracts.Exceptions;

namespace Movies.Application.Commands
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Unit>
    {
        public readonly MovieDbContext _movieDbContext;
        public UpdateMovieCommandHandler(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }
        public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {

            var movieToUpdate = await _movieDbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == request.Id, cancellationToken);
            if (movieToUpdate is null)
            {
                throw new NotFoundException($"{nameof(Movie)} with {nameof(Movie.Id)}:{request.Id}" +
                     $"Was not found in database");
            }
            movieToUpdate.Description = request.Description;
            movieToUpdate.Title = request.Title;
            movieToUpdate.Category = request.Category;
            _movieDbContext.Movies.Update(movieToUpdate);
            await _movieDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
