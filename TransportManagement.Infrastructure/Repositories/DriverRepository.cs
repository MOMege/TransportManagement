using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Interfaces;
using TransportManagement.Domain.Entites;
using TransportManagement.Infrastructure.Persistence;

namespace TransportManagement.Infrastructure.Repositories
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        public DriverRepository(TransportDbContext dbContext) : base(dbContext)
        {
        }

        public async  Task<bool> IsPhoneNumberExist(string phoneNumber)
        {
            return await _dbset.AnyAsync(x => x.PhoneNumber == phoneNumber);
        }
    }
}
