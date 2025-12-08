using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Driveres;
using TransportManagement.Application.DTOs.Vehicles;
using TransportManagement.Domain.Entites;


namespace TransportManagement.Application.Mapping
{
    public class DriverProfile :Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverDto>();


            CreateMap<UpdateDriverDto, Driver>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
            CreateMap<UpdateDriverDto, Driver>().
                ConstructUsing(src => new Driver(src.FullName, src.PhoneNumber,src.IsActive));

        }
    }
}
