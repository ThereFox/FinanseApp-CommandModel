using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs.BillChange
{
    internal class BillChangeConfiguration : IEntityTypeConfiguration<BillChangeEntity>
    {
        public void Configure(EntityTypeBuilder<BillChangeEntity> builder)
        {

            builder.ToTable("BillChanges");

            builder
                .HasKey(ex => ex.Id);


            builder
                .Property(ex => ex.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(ex => ex.Bill)
                .WithMany(ex => ex.ChangesAfterSnapshot)
                .HasPrincipalKey(e => e.Id)
                .HasForeignKey(ex => ex.BillId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(ex => ex.ExecutionContext)
                .WithMany(ex => ex.Changes)
                .HasPrincipalKey(ex => ex.Id)
                .HasForeignKey(ex => ex.TransactionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(ex => ex.Change)
                .IsRequired();

            builder
                .Property(ex => ex.CreateDate)
                .HasColumnType("TIMESTAMPTZ");
        }
    }
}
