using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.AudiltLog;
using TransportManagement.Application.Wrappers;

namespace TransportManagement.Application.Features.AuditLog
{
    public record GetHistoryByRecordIdQuery(Guid RecordId, string TableName) : IRequest<List<AuditLogDto>>
    {
    }
}
