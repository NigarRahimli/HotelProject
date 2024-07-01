using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Entities;

namespace Project.DataAccessLayer.Configurations
{
    public class FacilityCountEntityTypeConfiguration : IEntityTypeConfiguration<FacilityCount>
    {
        public void Configure(EntityTypeBuilder<FacilityCount> builder)
        {
            builder.Property(m => m.Id)
                   .HasColumnType("int")
                   .UseIdentityColumn(1, 1)
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.Count)
                   .HasColumnType("int")
                   .HasDefaultValue(0);

            builder.Property(m => m.FacilityId)
                   .HasColumnType("int");

            builder.Property(m => m.PropertyId)
                   .HasColumnType("int");

            builder.ConfigureAuditable();
            builder.HasKey(m => new { m.PropertyId, m.FacilityId });

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
