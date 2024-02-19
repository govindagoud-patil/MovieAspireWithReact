using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities
{
    public class MovieUser : IdentityUser
    {
        public string? IsRatingAllowed { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
