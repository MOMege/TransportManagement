using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TransportManagement.Application.Comman.Pagination;
using TransportManagement.Application.DTOs.Driveres;
using TransportManagement.Application.DTOs.Vehicles;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Wrappers;
using TransportManagement.Domain.Entites;


namespace TransportManagement.Application.Features.Vehicles.Queries.GetAllVehicles;

public class GetAllVehiclesQueryHandler 
    : IRequestHandler<GetAllVehiclesQuery, Result< PagedResult<VehicleListDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public GetAllVehiclesQueryHandler(IUnitOfWork unitOfWork,IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _paginationService = paginationService;
    }

    public async Task< Result< PagedResult<VehicleListDto>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        /* without paggnation 
        var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
        var dtos= _mapper.Map<IEnumerable<VehicleListDto>>(vehicles);

        return dtos;
        */
        /* with manual pagination not generic
        var query =  _unitOfWork.Vehicles.Query()
            .Where(v => !v.IsDeleted);
        // 🔍 Filtering
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(v =>
               v.PlateNumber.Contains(request.Search)
               || v.DoorNumber.ToString().Contains(request.Search));
        }
        if (request.IsActive.HasValue)
        {
            query = query.Where(v => v.IsActive == request.IsActive);
        }

        // ↕ Sorting
        query = request.OrderBy switch
        {
            "PlateNumber" => request.IsDescending ?
            query.OrderByDescending(v => v.PlateNumber)
            : query.OrderBy(v => v.PlateNumber),

            "DoorNumber" => request.IsDescending?
            query.OrderByDescending(v=> v.DoorNumber):
            query.OrderBy(v => v.DoorNumber),


            _ => query.OrderByDescending(v => v.CreatedAt)
        };

        // 📄 Pagination
        var totalcount = await query.CountAsync(cancellationToken);
        var items= await query
            .Skip((request.PageNumber -1) * request.PageNumber)
            .Take(request.PageSize)
            .ProjectTo<VehicleListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        var pageResult =  new PagedResult<VehicleListDto>
        {

            TotalCount = totalcount,
            Items = items,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        return  Result<PagedResult<VehicleListDto>>
            .Success(pageResult, "Vehicles retrieved successfully");*/

        var query = _unitOfWork.Vehicles.Query()
             .Where(v => !v.IsDeleted);
        var querydto = query.ProjectTo<VehicleListDto>(_mapper.ConfigurationProvider);
        var pageresult = await _paginationService.CreateAsync<VehicleListDto>
            (
            querydto,
            request.PageNumber,
            request.PageSize,
            cancellationToken

            );
        return Result<PagedResult<VehicleListDto>>.Success( pageresult );
    }
}
