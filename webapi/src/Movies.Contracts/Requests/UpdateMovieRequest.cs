using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Requests
{
    public record UpdateMovieRequest(int Id, string Title, string Description, string Category);

}
