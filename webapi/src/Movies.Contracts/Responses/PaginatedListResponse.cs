using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses
{
    public class PaginatedListResponse<T>
    {

        public PaginatedListResponse(int totalCount, int pageSize, int pageNumber, IEnumerable<T> items)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            CurrentPage = pageNumber;
            PageNumber = pageNumber;
            Items = items;
        }

        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int CurrentPage { get; set; }

        public IEnumerable<T> Items { get; set; }

    }
}
