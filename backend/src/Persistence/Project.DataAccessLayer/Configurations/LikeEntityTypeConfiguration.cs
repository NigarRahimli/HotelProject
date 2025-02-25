﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Entities.Membership;

namespace Project.DataAccessLayer.Configurations
{
     class LikeEntityTypeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.Property(l => l.Id).HasColumnType("int").UseIdentityColumn(1, 1);

            builder.Property(l => l.UserId)
                   .HasColumnType("int");

            builder.Property(l => l.PropertyId)
                   .HasColumnType("int");

            builder.HasOne<AppUser>()
                   .WithMany()
                   .HasPrincipalKey(m => m.Id)
                   .HasForeignKey(m => m.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Property>()
                   .WithMany()
                   .HasPrincipalKey(m => m.Id)
                   .HasForeignKey(m => m.PropertyId)
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasIndex(l => new { l.UserId, l.PropertyId }).IsUnique();
            builder.HasKey(m => m.Id);
            builder.ToTable("Likes");
        }

    }
}

