using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransportManagement.Application.Features.AuditLog;

namespace TransportManagement.Api.Controllers
{
    [Controller]
    public class AuditLogsController : Controller
    {
        private readonly IMediator _mediator;
        public AuditLogsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("history/{tableName}/{recordId:guid}")]
        public async Task<IActionResult> GetHistory(string tableName, Guid recordId)
        {
            var result = await _mediator.Send(
                new GetHistoryByRecordIdQuery(recordId, tableName)
            );
            return Ok(result);
        }

    }
}
