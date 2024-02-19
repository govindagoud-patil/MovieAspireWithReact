using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Commands
{
    public record UpdateMovieCommand(int Id,string Title,String Description,string Category): IRequest<Unit>;

}
