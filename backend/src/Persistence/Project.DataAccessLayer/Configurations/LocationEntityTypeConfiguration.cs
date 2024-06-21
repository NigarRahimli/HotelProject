using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Entities;

namespace Project.DataAccessLayer.Configurations
{
    public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(m => m.Id)
                   .HasColumnType("int")
                   .UseIdentityColumn(1, 1);

            builder.Property(m => m.Latitude)
                   .HasColumnType("decimal(10, 7)") 
                   .IsRequired();

            builder.Property(m => m.Longitude)
                   .HasColumnType("decimal(10, 7)") 
                   .IsRequired();

            builder.Property(m => m.Address)
                   .IsRequired();

            builder.Property(m => m.City)
                   .IsRequired();

            builder.Property(m => m.State)
                   .IsRequired();

            builder.Property(m => m.Country)
                   .IsRequired();

            builder.Property(m => m.ZipCode)
                   .IsRequired();

            builder.ConfigureAuditable(); 

            builder.HasKey(m => m.Id);

            builder.ToTable("Locations");
        }
    }
}
