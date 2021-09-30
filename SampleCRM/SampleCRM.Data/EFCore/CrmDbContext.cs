using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SampleCRM.Entities;
using SampleCRM.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCRM.Data.EFCore
{
    public class CrmDbContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }

        public CrmDbContext(DbContextOptions<CrmDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CrmDbContext>
        {
            public CrmDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../SampleCRM.API/appsettings.development.json").Build();
                var builder = new DbContextOptionsBuilder<CrmDbContext>();
                var connectionString = configuration.GetConnectionString("SQLServer");
                builder.UseSqlServer(connectionString);
                return new CrmDbContext(builder.Options);
            }
        }

    }
}
