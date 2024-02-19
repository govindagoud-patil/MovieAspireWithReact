using Microsoft.EntityFrameworkCore;
using Movies.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Helpers
{
    public static class CollectionHelpers<T>
    {
        public static async Task<PaginatedListResponse<T>> ToPaginatedList(IQueryable<T> source, int pageNubmer,int pageSize)
        {
            var totalCount = await source.CountAsync();            
            var items = await source.Skip((pageNubmer - 1) * pageSize).Take(pageSize).ToListAsync();
            return   new PaginatedListResponse<T>( totalCount,  pageSize,pageNubmer, items);
        }
    }
}
