using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Interfaces
{
    public interface IDriverRepository : IRepository<Driver>
    {
    
        Task<bool> IsPhoneNumberExist(string phoneNumber);
    }
}
