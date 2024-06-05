using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Domain.Models.Entities;

namespace Project.DataAccessLayer.Configurations
{
    public class PropertyEntityTypeConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();          
            builder.Property(m=>m.DescriptionId).HasColumnType("int");
            builder.Property(m=>m.KindId).HasColumnType("int");
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


        }
    }
}
