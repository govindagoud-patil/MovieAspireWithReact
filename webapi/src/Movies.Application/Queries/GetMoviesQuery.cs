using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Movies.Contracts.Responses;

namespace Movies.Application.Queries;

public class GetMoviesQuery: IRequest<GetMoviesResponse>{}
