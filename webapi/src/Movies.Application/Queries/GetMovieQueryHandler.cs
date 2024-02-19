using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Contracts.Responses;
using Movies.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Queries;

public class GetMovieQueryHandler : IRequestHandler<GetMoviesQuery, GetMoviesResponse>
{
    public readonly MovieDbContext _movieDbContext;
    public GetMovieQueryHandler(MovieDbContext movieDbContext)
    {
        _movieDbContext = movieDbContext;
    }

    public async Task<GetMoviesResponse> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
      var movies= await _movieDbContext.Movies.ToListAsync().ConfigureAwait(false);
        return movies.Adapt<GetMoviesResponse>();
    }
}
