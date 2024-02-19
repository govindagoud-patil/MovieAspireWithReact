using Mapster;
using MediatR;
using Movies.Application.Helpers;
using Movies.Contracts.Dtos;
using Movies.Contracts.Responses;
using Movies.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Queries
{
    public class GetPaginatedListMovieQueryHandler : IRequestHandler<GetPaginatedListMoviesQuery, PaginatedListResponse<MovieDto>>
    {
        public readonly MovieDbContext _movieDbContext;
        public GetPaginatedListMovieQueryHandler(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }
        public async Task<PaginatedListResponse<MovieDto>> Handle(GetPaginatedListMoviesQuery request, CancellationToken cancellationToken)
        {
            var getMovieQuery = _movieDbContext.Movies.ProjectToType<MovieDto>().AsQueryable();
            var paginatedList = await CollectionHelpers<MovieDto>.ToPaginatedList(getMovieQuery,request.paginationParamsRequest.PageNumber,request.paginationParamsRequest.PageSize);
            return paginatedList;
        }
    }
}
