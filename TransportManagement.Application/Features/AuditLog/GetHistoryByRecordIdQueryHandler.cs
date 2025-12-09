using MediatR;
using TransportManagement.Application.DTOs.AudiltLog;
using TransportManagement.Application.Interfaces;

namespace TransportManagement.Application.Features.AuditLog
{
    public class GetHistoryByRecordIdQueryHandler
        : IRequestHandler<GetHistoryByRecordIdQuery, List<AuditLogDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHistoryByRecordIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AuditLogDto>> Handle(
            GetHistoryByRecordIdQuery request,
            CancellationToken cancellationToken)
        {
            var logs = await _unitOfWork.AuditLogs.FindAsync(
                x => x.RecordId == request.RecordId
            );

            return logs
                .OrderByDescending(x => x.ActionDate)
                .Select(x => new AuditLogDto
                {
                    Id = x.Id,
                    TableName = x.TableName,
                    RecordId = x.RecordId,
                    ActionType = x.ActionType,
                    UserName = x.UserName,
                    ActionDate = x.ActionDate,
                    OldValues = x.OldValues,
                    NewValues = x.NewValues
                })
                .ToList();
        }
    }
}
