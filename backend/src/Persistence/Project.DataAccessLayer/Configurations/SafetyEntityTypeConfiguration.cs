using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Models.Entities;


namespace Project.DataAccessLayer.Configurations
{
    class SafetyEntityTypeConfiguration : IEntityTypeConfiguration<Safety>
    {
        public void Configure(EntityTypeBuilder<Safety> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(m => m.IconUrl).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();

            builder.ConfigureAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("Safeties");
        }

    }
}
