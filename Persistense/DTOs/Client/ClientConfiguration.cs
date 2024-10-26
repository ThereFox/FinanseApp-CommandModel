using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs.Client
{
    public class ClientConfiguration : IEntityTypeConfiguration<ClientEntity>
    {
        public void Configure(EntityTypeBuilder<ClientEntity> builder)
        {
            builder
                .ToTable("Clients");

            builder
                .HasKey(ex => ex.Id);

            builder
                .Property(ex => ex.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(ex => ex.Name);

            builder
                .HasMany(ex => ex.Bills)
                .WithOne(ex => ex.Owner)
                .HasPrincipalKey(ex => ex.Id)
                .HasForeignKey(ex => ex.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
