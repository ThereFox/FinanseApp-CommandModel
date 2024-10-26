using Microsoft.EntityFrameworkCore;
using Persistense.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<BillEntity> Bills { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }

        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
        }

    }
}
