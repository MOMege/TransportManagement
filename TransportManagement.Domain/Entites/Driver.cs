using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain;


namespace TransportManagement.Domain.Entites
{
    public class Driver :BaseEntity,IAggregateRoot
    {
        //property
    
        public string FullName { get; private set; } = default!;
        public string PhoneNumber {  get; private set; }=default!;
        public bool IsActive { get; private set; } = true;
       

     
        //Ef core 
        private Driver()
            { }
        public Driver(string fullName, string phoneNUmber)
        {
            this.FullName = fullName;
            this.PhoneNumber = phoneNUmber;
            IsActive = true;
           
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

        public void  Update(string fullName , string phoneNUmber, bool isactive)
        {
            FullName = fullName;
            PhoneNumber = phoneNUmber;
            IsActive = isactive;

        }
         
        public void ToggleActivation()
        { IsActive = !IsActive; }

        //Factory Method For Unit Test
        public static Driver Create(string fullName, string phoneNumber, bool isActive = true)
        {
            return new Driver
            {
                Id = Guid.NewGuid(),
                FullName = fullName,
                PhoneNumber = phoneNumber,
                IsActive = isActive
            };
        }
        //navegations

        public ICollection<Trip> trips { get; private set; } =new List<Trip>();
    }
}
