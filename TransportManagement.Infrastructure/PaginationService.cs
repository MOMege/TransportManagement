using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Comman.Pagination;

namespace TransportManagement.Infrastructure
{
    public class PaginationService : IPaginationService
    {
        public async Task<PagedResult<T>> CreateAsync<T>
            (IQueryable<T> query, int PageNumber, int PageSize, CancellationToken cancellationToken = default)
        {
            var totalcount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<T> {
              PageNumber=PageNumber,
              Items=items
              ,PageSize=PageSize
              ,TotalCount=totalcount
               };
               

        }
    }
}
