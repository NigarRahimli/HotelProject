using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Models.Entities;

namespace Project.DataAccessLayer.Configurations
{
    class PropertyImageEntityTypeConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.PropertyId).HasColumnType("int").IsRequired();
            builder.Property(m => m.Image).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(m => m.Url).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();

            builder.HasOne<Property>()
                .WithMany()
                .HasPrincipalKey(m => m.Id)
                .HasForeignKey(m => m.PropertyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ConfigureAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("PropertyImages");
        }

    }
}
