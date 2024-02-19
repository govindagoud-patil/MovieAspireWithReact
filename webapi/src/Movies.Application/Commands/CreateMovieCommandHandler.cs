using MediatR;
using Movies.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Domain.Entities;

namespace Movies.Application.Commands
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
    {
        public readonly MovieDbContext _movieDbContext;
        public CreateMovieCommandHandler(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }
        public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = new Movie { Title = request.Title, Category = request.Category, Description = request.Description };
            await _movieDbContext.Movies.AddAsync(movie);
            await _movieDbContext.SaveChangesAsync(cancellationToken);
            return movie.Id;

        }
    }
}
