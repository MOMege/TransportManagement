using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Enums;

namespace TransportManagement.Domain.Entites
{
    public class Trip :BaseEntity ,IAggregateRoot
    {

        //property
  
        public string TripNumber { get; private set; } = default!;
        public Guid VehicleId { get; private set; }
        public Guid DriverId { get; private set; }

        public string Origin { get; private set; } = default!;
        public string Destination { get; private set; } = default!;
        public DateTime PlannedStartDate { get; private set; }
        public DateTime? ActualStartDate { get; private set; }
        public DateTime? ActualEndDate { get; private set; }

        public TripStatus Status { get; private set; } = TripStatus.Planned;

        // حمل الشحنة بالوزن والتكلفة الأساسية
        public decimal LoadWeightKg { get; private set; }
        public decimal BasePrice { get; private set; }

        // constructors

        private Trip() { }

        public Trip(
            string tripNumber,
            Guid vehicleId,
            Guid driverId,
            string origin,
            string destination,
            DateTime plannedStartDate,
            decimal loadWeightKg,
            decimal basePrice)
        {
            TripNumber = tripNumber;
            VehicleId = vehicleId;
            DriverId = driverId;
            Origin = origin;
            Destination = destination;
            PlannedStartDate = plannedStartDate;
            LoadWeightKg = loadWeightKg;
            BasePrice = basePrice;
        }

        //navegation many to one 
        public Vehicle Vehicle { get; private set; } = default!;
        public Driver Driver { get; private set; } = default!;



        //methods 

        public void StartTrip()
        {
            if (Status != TripStatus.Planned)
                throw new InvalidOperationException("Trip can be started only from Planned state.");

            Status = TripStatus.InProgress;
            ActualStartDate = DateTime.UtcNow;
            MarkUpdated();
        }

        public void CompleteTrip()
        {
            if (Status != TripStatus.InProgress)
                throw new InvalidOperationException("Trip can be completed only from InProgress state.");

            Status = TripStatus.Completed;
            ActualEndDate = DateTime.UtcNow;
            MarkUpdated();
        }

        public void CancelTrip(string? reason = null)
        {
            if (Status == TripStatus.Completed)
                throw new InvalidOperationException("Completed trip cannot be canceled.");

            Status = TripStatus.Canceled;
            MarkUpdated();
            // ممكن نحفظ سبب الإلغاء في future property
        }
    }
}
