using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Features.Export.Enums;

namespace TransportManagement.Application.Features.Export.Interfaces
{
    public interface IExportService
    {
        Task<byte[]> ExportDriversAsync(ExportFormat format);
        Task<byte[]> ExportVehiclesAsync(ExportFormat format);
        Task<byte[]> ExportTripsAsync(ExportFormat format);
    }
}
