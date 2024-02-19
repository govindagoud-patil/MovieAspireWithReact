using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Contracts;
using Movies.Contracts.Exceptions;
using Movies.Contracts.Responses;
using Movies.Domain.Entities;
using Movies.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Queries
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, GetMovieByIdResponse>
    {
        public readonly MovieDbContext _movieDbContext;
        public GetMovieByIdQueryHandler(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        public async Task<GetMovieByIdResponse> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await _movieDbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == request.Id , cancellationToken);

            if (movie is null)
            {
                throw new NotFoundException($"{nameof(Movie) } with {nameof(Movie.Id)}:{request.Id}" +
                    $"Was not found in database");
            }
            return movie.Adapt<GetMovieByIdResponse>();
        }
    }
}
