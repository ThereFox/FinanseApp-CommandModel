using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs.BillStateSnapshot
{
    public class BillStateSnapshotConfiguration : IEntityTypeConfiguration<BillStateSnapshotEntitys>
    {
        public void Configure(EntityTypeBuilder<BillStateSnapshotEntitys> builder)
        {
            builder.ToTable("Snapshots");

            builder
                .HasKey(ex => ex.Id);


            builder
                .Property(ex => ex.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(ex => ex.Bill)
                .WithMany(ex => ex.StateSnapshots)
                .HasPrincipalKey(ex => ex.Id)
                .HasForeignKey(ex => ex.BillId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(ex => ex.Amount);

            builder
                .Property(ex => ex.CreateDate)
                .HasColumnType("TIMESTAMPTZ")
                .IsRequired();

        }
    }
}
