using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Domain.Entites
{
    public class Invoice :BaseEntity ,IAggregateRoot 
    {
        public string InvoiceNumber { get; private set; } = default!;
        public Guid TripId { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Tax { get; private set; }
        public decimal Total => SubTotal + Tax;
        public bool IsPaid { get; private set; }

        // Navigation
        public Trip Trip { get; private set; } = default!;

        private Invoice() { }

        public Invoice(string invoiceNumber, Guid tripId, decimal subTotal, decimal tax)
        {
            InvoiceNumber = invoiceNumber;
            TripId = tripId;
            SubTotal = subTotal;
            Tax = tax;
            IsPaid = false;
        }

        public void MarkAsPaid()
        {
            IsPaid = true;
            MarkUpdated();
        }
    }
}
