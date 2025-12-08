using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Interfaces;
using TransportManagement.Domain.Entites;
using TransportManagement.Infrastructure.Persistence;
using TransportManagement.Infrastructure.Repositories;

namespace TransportManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly TransportDbContext _dbcontext;
        public IVehicleRepository  Vehicles { get; }

        public ITripRepository Trips { get; }

        public IRepository<Invoice> Invoices { get; }

        public IRepository<Driver> Drivers { get; }

      
        public UnitOfWork(TransportDbContext dbContext)
        {
            _dbcontext = dbContext;
            Vehicles= new VehicleRepository(_dbcontext); // have sepecial reposatory
            Trips = new TripRepository(_dbcontext);
            Invoices = new Repository<Invoice>(_dbcontext);
            Drivers = new Repository<Driver>(_dbcontext);


        }
        public async Task<int> SaveChangesAsync()
            => await _dbcontext.SaveChangesAsync();
   

        public void Dispose()
        {
            _dbcontext.Dispose();
        }

        


    }
}
