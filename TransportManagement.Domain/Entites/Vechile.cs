using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Enums;

namespace TransportManagement.Domain.Entites
{
    public class Vehicle : BaseEntity,IAggregateRoot  //it's root &inherint from base class
    {
  
        //property 

        public int DoorNumber { get; private set; }
        public string PlateNumber { get; private set; } = string.Empty;
        public VechileType Type { get; private set; }

        public decimal MaxLoadKg {  get; private set; }
       
        public bool IsActive {  get; private set; }=true;

        //constructor 

      
        public Vehicle(string plateNumber,VechileType vechileType,int doornum, decimal maxLoadKg)
        {
            this.DoorNumber = doornum;
            this.PlateNumber = plateNumber;
            this.Type = vechileType;
            this.MaxLoadKg = maxLoadKg;
            this.IsActive = true;
        }
        //DDD / Clean Architecture…
       // إحنا عادة بنمنع إنشاء الكيان مباشرة من خارج الدومين، لذلك نخلي الـ constructor العام private أو protected
       // EF core
                private Vehicle() { }

        public void Deactivate() => IsActive = false; 
        public void Active() =>IsActive=true;
        // Navegation 

        public ICollection<Trip> Trips { get; private set;} = new List<Trip>();

        public void UpdateDetails(int doorNumber, VechileType type, decimal maxLoadKg)
        {
            DoorNumber = doorNumber;
            Type = type;
            MaxLoadKg = maxLoadKg;
        }
        public void Update(string plateNumber, VechileType type, decimal maxLoadKg, int doorNumber)
        {
            PlateNumber = plateNumber;
            Type = type;
            MaxLoadKg = maxLoadKg;
            DoorNumber = doorNumber;
        }
        public void Deactived() => IsActive = false;
        public void Activated() => IsActive = true;
    }
}
