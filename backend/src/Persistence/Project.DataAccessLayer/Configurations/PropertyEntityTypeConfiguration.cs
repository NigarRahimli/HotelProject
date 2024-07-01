using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Entities;

namespace Project.DataAccessLayer.Configurations
{
    public class PropertyEntityTypeConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(m => m.Id)
                   .HasColumnType("int")
                   .UseIdentityColumn(1, 1);

            builder.Property(m => m.Name)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(m => m.GuestNum)
                   .HasColumnType("int");

            builder.Property(m => m.DescriptionId)
                   .HasColumnType("int");

            builder.Property(m => m.Rate)
                   .HasColumnType("float")
                   .HasDefaultValue(0.0);

            builder.Property(m => m.LongPrice)
                   .HasColumnType("float");

            builder.Property(m => m.MedPrice)
                   .HasColumnType("float");

            builder.Property(m => m.MinPrice)
                   .HasColumnType("float");

            builder.Property(m => m.IsPetFriendly)
                   .HasColumnType("bit");

            builder.Property(m => m.KindId)
                   .HasColumnType("int");

            builder.Property(m => m.LocationId)
                   .HasColumnType("int");

            builder.ConfigureAuditable();
            builder.HasKey(m => m.Id);

            builder.HasOne<Description>()
                   .WithMany()
                   .HasPrincipalKey(m => m.Id)
                   .HasForeignKey(m => m.DescriptionId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Kind>()
                   .WithMany()
                   .HasPrincipalKey(m => m.Id)
                   .HasForeignKey(m => m.KindId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Location>()
                   .WithMany()
                   .HasPrincipalKey(m => m.Id)
                   .HasForeignKey(m => m.LocationId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Properties", t =>
            {
                t.HasCheckConstraint("CK_Property_Rate", "[Rate] >= 0 AND [Rate] <= 5");
                t.HasCheckConstraint("CK_Property_Prices", "[LongPrice] >= [MinPrice] AND [LongPrice] >= [MedPrice] AND [MedPrice] >= [MinPrice]");
            });
        }
    }
}
