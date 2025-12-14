using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Comman.Pagination;
using TransportManagement.Application.DTOs.Driveres;
using TransportManagement.Application.Features.Vehicles.Queries.GetAllVehicles;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Wrappers;
using TransportManagement.Domain.Entites;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TransportManagement.Application.Features.Drivers.Queires.GetAllDrivers
{
    public class GetAllDriverQueryHandler : IRequestHandler<GetAllDriverQuery, Result<PagedResult<DriverDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllDriverQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) { 
        _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
       
        public async Task <Result<PagedResult<DriverDto>>> Handle(GetAllDriverQuery request, CancellationToken cancellationToken)
        {

            /* in case Without Pagginaation 
           var Drivers= await _unitOfWork.Drivers.GetAllAsync();
            var listdto=_mapper.Map <IEnumerable < DriverDto >> (Drivers);
            return Result<< IEnumerable<DriverDto>>>.Success(listdto, "All Drivers");
            */

            /// <summary>
            /// paggnation
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns> 
            /// 
            var query = _unitOfWork.Drivers.Query().
                Where(x => !x.IsDeleted);
            // 🔍 Filtering
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(d =>
                    d.FullName.Contains(request.Search) ||
                    d.PhoneNumber.Contains(request.Search));
            }

            if (request.IsActive.HasValue)
            {
                query = query.Where(d => d.IsActive == request.IsActive);
            }

            // ↕ Sorting
            query = request.OrderBy switch
            {
                "FullName" => request.IsDescending
                    ? query.OrderByDescending(d => d.FullName)
                    : query.OrderBy(d => d.FullName),

                _ => query.OrderByDescending(d => d.CreatedAt)
            };


            // 📄 Pagination
            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var pageResult = new PagedResult<DriverDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            } ;
            return Result<PagedResult <DriverDto>>
        .Success(pageResult, "Drivers retrieved successfully");
        }
    }
}
