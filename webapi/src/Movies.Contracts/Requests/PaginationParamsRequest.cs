using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Requests
{
    public class PaginationParamsRequest
    {
        public int PageSize
        {

            get { return _pageSize; }
            set
            {
                _pageSize = value > MaximumPagSize ? MaximumPagSize : value;
            }
        }

        public int _pageSize = 5;
        public int PageNumber { get; set; } = 1;

        public const int MaximumPagSize = 30;
    }
}
