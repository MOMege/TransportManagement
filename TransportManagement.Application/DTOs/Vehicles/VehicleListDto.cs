using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Enums;

namespace TransportManagement.Application.DTOs.Vehicles
{
   
        public class VehicleListDto
        {
            public string PlateNumber { get; set; }
            public VechileType Type { get; set; }
            public int DoorNumber { get; set; }
            public decimal MaxLoadKg { get; set; }
            public bool IsActive { get; set; }
        }
    
}
