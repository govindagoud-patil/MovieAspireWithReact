using MediatR;
using Movies.Contracts.Dtos;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Queries
{
    public record GetPaginatedListMoviesQuery(PaginationParamsRequest paginationParamsRequest) : IRequest<PaginatedListResponse<MovieDto>>
    {
    }
}
