using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Movies.Contracts.Responses;
using Serilog.Context;

namespace Movies.Application.Behaviors
{
    internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>
     : IPipelineBehavior<TRequest, TResponse>
     {
        private readonly ILogger _logger;
        public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger )
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;

            _logger.LogInformation(
                "Processing request {RequestName}",
                requestName);

            TResponse response;

            try
            {

                response = await next();

            }
            finally
            {
                _logger.LogInformation(
                 "Completed request {RequestName}",
                 requestName);

            }

            return response;
        }
    }
}
