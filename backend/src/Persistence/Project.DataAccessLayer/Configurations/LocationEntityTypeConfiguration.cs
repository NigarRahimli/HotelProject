using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Project.Domain.Models.Entities;

namespace Project.DataAccessLayer.Configurations
{
    public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Models.Entities.Location>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Entities.Location> builder)
        {

            builder.Property(m => m.Id)
                   .HasColumnType("int")
                   .UseIdentityColumn(1, 1);

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

         
            builder.Property(m => m.Coordinates)
                   .HasColumnType("nvarchar(max)")
                   .HasConversion(
                       v => $"{v.X},{v.Y}",
                       v => PointConverter.FromString(v))
                   .IsRequired();

            builder.ConfigureAuditable();

            builder.HasKey(m => m.Id);

            builder.ToTable("Locations");
        }
    }

    public static class PointConverter
    {
        public static Point FromString(string value)
        {
            var coordinates = value.Split(',');
            if (coordinates.Length == 2 && double.TryParse(coordinates[0], out double x) && double.TryParse(coordinates[1], out double y))
            {
                return new Point(x, y) { SRID = 4326 };
            }
            else
            {
                return null; 
            }
        }
    }
}
