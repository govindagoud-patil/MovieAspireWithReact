using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Commands;
using Movies.Application.Queries;
using Movies.Contracts;
using Movies.Contracts.Dtos;
using Movies.Contracts.Requests;
using Movies.Infrastructure;

namespace Movies.webapi.Extensions
{
    public static class PresentationExtensions
    {
        public static void AddMovieEndpoints(this IEndpointRouteBuilder route,bool isSecuredEndpoint)
        {
            var MoviesRoute = route.MapGroup("/api/movies").WithTags("Movies");
            if (isSecuredEndpoint)
            {
                MoviesRoute.RequireAuthorization();
            }

            MoviesRoute.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
            {
                var movies = await mediator.Send(new GetMoviesQuery(), ct);
                return Results.Ok(movies);
            });

            MoviesRoute.MapGet("/paginated", async (IMediator mediator, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken ct) =>
            {
                var movies = await mediator.Send(new GetPaginatedListMoviesQuery(new PaginationParamsRequest() { PageNumber = pageNumber, PageSize = pageSize }), ct);
                return Results.Extensions.OkPaginatedResults(movies);
            });

            MoviesRoute.MapGet("/{id}", async (IMediator mediator, int id, CancellationToken ct) =>
            {
                var movie = await mediator.Send(new GetMovieByIdQuery(id), ct);
                return Results.Ok(movie);
            });

            MoviesRoute.MapPost("/", async (IMediator mediator, CreateMovieRequest createMovieRequest, CancellationToken ct) =>
            {
                var createMovieCommand = new CreateMovieCommand(createMovieRequest.Title, createMovieRequest.Description, createMovieRequest.Category);
                var movie = await mediator.Send(createMovieCommand, ct);
                return Results.Ok(movie);
            });

       
            MoviesRoute.MapPut("/{id}", async (IMediator mediator, int id, UpdateMovieRequest createMovieRequest, CancellationToken ct) =>
            {

                var updateMovieCommand = new UpdateMovieCommand(id, createMovieRequest.Title, createMovieRequest.Description, createMovieRequest.Category);
                var movie = await mediator.Send(updateMovieCommand, ct);
                return Results.Ok(movie);
            });

            MoviesRoute.MapDelete("/{id}", async (IMediator mediator, int id, CancellationToken ct) =>
            {
                var deleteMovieCommand = new DeleteMovieCommand(id);
                var movie = await mediator.Send(deleteMovieCommand, ct);
                return Results.Ok(movie);
            });

        }

    }
}
