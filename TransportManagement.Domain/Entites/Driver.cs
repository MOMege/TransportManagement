using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Domain.Entites
{
    public class Driver :BaseEntity,IAggregateRoot
    {
        //property
    
        public string FullName { get; protected set; } = default!;
        public string PhoneNumber {  get; protected set; }=default!;
        public bool IsActive { get; protected set; } = true;
        //Ef core 
        private Driver()
            { }
        public Driver(string fullName, string phoneNUmber)
        {
            this.FullName = fullName;
            this.PhoneNumber = phoneNUmber;
           
        }
        public Driver(string fullName, string phoneNUmber,bool isactive)
        {
            this.FullName = fullName;
            this.PhoneNumber = phoneNUmber;
            this.IsActive = isactive;

        }
        //methods

        public void Deactivate()=> IsActive = false;
        public void Activate()=> IsActive= true;

        //navegations

        public ICollection<Trip> trips { get; private set; } =new List<Trip>();
    }
}
