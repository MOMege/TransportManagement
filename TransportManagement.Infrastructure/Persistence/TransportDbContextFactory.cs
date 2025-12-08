using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace TransportManagement.Infrastructure.Persistence
{
    public class TransportDbContextFactory : IDesignTimeDbContextFactory<TransportDbContext>
    {
        public TransportDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: true)
                      .AddJsonFile("appsettings.Development.json", optional: true)
                      .Build();
            var optionsBuilder = new DbContextOptionsBuilder<TransportDbContext>();

            var connectionString = config.GetConnectionString("Dbconnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new TransportDbContext(optionsBuilder.Options);
        }
    }
}
