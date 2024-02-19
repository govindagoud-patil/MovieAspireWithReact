using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Commands
{
    public record CreateMovieCommand(string Title,string Description,string Category): IRequest<int>;

}
