using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.DTOs.AudiltLog
{
    public class AuditLogDto
    {
        public Guid Id { get; set; }
        public Guid RecordId { get; set; }
        public string TableName { get; set; } = default!;
        public string ActionType { get; set; } = default!;
        public string? UserName { get; set; }
        public DateTime ActionDate { get; set; }
        public Dictionary<string, object>? OldValues { get; set; }
        public Dictionary<string, object>? NewValues { get; set; }
    }

}
