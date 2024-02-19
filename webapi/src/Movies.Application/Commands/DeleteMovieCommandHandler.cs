using MediatR;
using Movies.Infrastructure;

using Movies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Movies.Contracts;
using Movies.Contracts.Exceptions;

namespace Movies.Application.Commands
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, Unit>
    {
        public readonly MovieDbContext _movieDbContext;
        public DeleteMovieCommandHandler(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }
        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {

            var MovieToDelete = await _movieDbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == request.Id, cancellationToken);

            if (MovieToDelete is null)
            {
                throw new NotFoundException($"{nameof(Movie)} with {nameof(Movie.Id)}:{request.Id}" +
                    $"Was not found in database");
            }
            _movieDbContext.Movies.Remove(MovieToDelete);
            await _movieDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
