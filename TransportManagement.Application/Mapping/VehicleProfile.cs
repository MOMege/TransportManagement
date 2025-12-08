using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Vehicles;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Mapping
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            // Domain → ResponseDTO
            CreateMap<Vehicle, VehicleDto>();

            // CreateRequestDTO → Domain
            CreateMap<CreateVehicleDto, Vehicle>()
                .ConstructUsing(dto =>
                    new Vehicle(dto.PlateNumber, dto.Type, dto.DoorNumber,dto.MaxLoadKg));

            // UpdateRequestDTO → Domain
            CreateMap<UpdateVehicleDto, Vehicle>()
                .ConstructUsing(src => new Vehicle(src.PlateNumber,src.Type,src.DoorNumber,src.MaxLoadKg));
            CreateMap<Vehicle, VehicleListDto>();


        }
    }
}
