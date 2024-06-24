using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Entities;

namespace Project.DataAccessLayer.Configurations
{
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {


            builder.Property(r => r.Id).HasColumnType("int").UseIdentityColumn(1, 1);


            builder.Property(r => r.Stars).IsRequired().HasColumnType("int");

            builder.HasOne<Property>()
                   .WithMany()
                   .HasPrincipalKey(m => m.Id)
                   .HasForeignKey(m => m.PropertyId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<ReviewCategory>()
                   .WithMany()
                   .HasPrincipalKey(m => m.Id)
                   .HasForeignKey(m => m.CategoryId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.ConfigureAuditable();

           
            builder.HasKey(r => r.Id);
            builder.ToTable("Reviews", t =>
            {
                t.HasCheckConstraint("CK_Review_Stars", "[Stars] BETWEEN 1 AND 5");
            });
        }
    }
}
