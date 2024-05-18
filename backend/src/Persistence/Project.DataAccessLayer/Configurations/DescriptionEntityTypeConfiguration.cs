
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Entities;

namespace Project.DataAccessLayer.Configurations
{

    public class DescriptionEntityTypeConfiguration : IEntityTypeConfiguration<Description>
    {
        public void Configure(EntityTypeBuilder<Description> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(200);

            builder.HasKey(m => m.Id);
            builder.ToTable("Descriptions"); 
        }

    }
}
