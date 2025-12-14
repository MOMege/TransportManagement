using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Comman.Pagination
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new ();
        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalPages { get; set; }
    }
}
