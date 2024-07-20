using Project.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Entities.Membership;

namespace Project.DataAccessLayer.Configurations
{
    public class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);

            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();

            builder.Property(m => m.CheckInTime).HasColumnType("datetime").IsRequired();
            builder.Property(m => m.CheckOutTime).HasColumnType("datetime").IsRequired();
            builder.Property(m => m.IsApproved).HasColumnType("bit").IsRequired();

            builder.Property(m => m.PropertyId).HasColumnType("int").IsRequired();

            builder.Property(m => m.ReservationStatus).HasColumnType("int").IsRequired();

            builder.HasKey(m => m.Id);

            builder.HasOne<Property>()
                 .WithMany()
                 .HasPrincipalKey(m => m.Id)
                 .HasForeignKey(m => m.PropertyId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.ConfigureAuditable();

            builder.ToTable("Reservations");
        }
    }
}
