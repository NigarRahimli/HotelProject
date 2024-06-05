using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccessLayer.Configurations
{
    public class FacilityCountEntityTypeConfiguration : IEntityTypeConfiguration<FacilityCount>
    {
        public void Configure(EntityTypeBuilder<FacilityCount> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Count).HasColumnType("int").HasDefaultValue(0);
            builder.Property(m => m.FacilityId).HasColumnType("int");
            builder.Property(m => m.PropertyId).HasColumnType("int");
            builder.ConfigureAuditable();
            builder.HasKey(m => m.Id);

   
            builder.HasOne<Property>()
                .WithMany()
                .HasPrincipalKey(m => m.Id)
                .HasForeignKey(m => m.PropertyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Facility>()
               .WithMany()
               .HasPrincipalKey(m => m.Id)
               .HasForeignKey(m => m.FacilityId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("FacilityCounts");
        }
    }
}
