using Movies.Contracts.Responses;
using System.Runtime.CompilerServices;
using System.Text.Json;


namespace Movies.webapi.Extensions
{
    public static class ResultsExtensions
    {
        public static IResult OkPaginatedResults<T>(this IResultExtensions resultExtensions,PaginatedListResponse<T> paginatedListResponse )
        {
            ArgumentNullException.ThrowIfNull(resultExtensions);
            return new PaginationResult<T>(paginatedListResponse);

        }

        public class PaginationResult<T>(PaginatedListResponse<T> paginatedListResponse) : IResult
        {
            public async Task ExecuteAsync(HttpContext httpContext)
            {
                var headers = new
                {
                    pageSize=paginatedListResponse.PageSize,
                    pageNumber=paginatedListResponse.PageNumber,
                    totalPages=paginatedListResponse.TotalPages,
                    totalItems=paginatedListResponse.TotalCount
                };

                httpContext.Response.Headers.Append("X-Pagination",JsonSerializer.Serialize(headers));
                httpContext.Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
                httpContext.Response.StatusCode = StatusCodes.Status200OK;
                await httpContext.Response.WriteAsJsonAsync(paginatedListResponse.Items);
            }
        }

    }
}
