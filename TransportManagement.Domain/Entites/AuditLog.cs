using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Domain.Entites
{
    public class AuditLog : BaseEntity
    {
        public string TableName { get; set; } = default!;
        public Guid RecordId { get; set; }

        public string ActionType { get; set; } = default!; // Create, Update, Delete

        public string? OldValues { get; set; }  // JSON
        public  string? NewValues { get; set; }  // JSON
        public string? ChangedColumns { get; set; }

        public string? UserName { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
