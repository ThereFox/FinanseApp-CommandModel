using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs
{
    public class BillConfiguration : IEntityTypeConfiguration<BillEntity>
    {
        public void Configure(EntityTypeBuilder<BillEntity> builder)
        {
            builder.ToTable("Bills");


            builder
                .Property(ex => ex.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasKey(ex => ex.Id);

            builder
                .HasOne(ex => ex.Owner)
                .WithMany(ex => ex.Bills)
                .HasPrincipalKey(ex => ex.Id)
                .HasForeignKey(ex => ex.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(ex => ex.ChangesAfterSnapshot)
                .WithOne(ex => ex.Bill)
                .HasPrincipalKey(ex => ex.Id)
                .HasForeignKey(ex => ex.BillId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(ex => ex.StateSnapshots)
                .WithOne(ex => ex.Bill)
                .HasPrincipalKey(ex => ex.Id)
                .HasForeignKey(ex => ex.BillId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
