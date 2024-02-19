using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Contracts.Dtos;

namespace Movies.Contracts.Responses;

public record GetMovieByIdResponse(MovieDto MovieDto);

