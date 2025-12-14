using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Comman.Pagination
{
    public interface IPaginationService
    {
        Task<PagedResult<T>> CreateAsync<T>(
            IQueryable<T> query,
            int PageNumber,
            int PageSize,
            CancellationToken cancellationToken = default(CancellationToken));
            
    
    }
}
