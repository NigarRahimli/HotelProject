﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Entities.Membership;

namespace Project.DataAccessLayer.Configurations.Membership
{
    class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.Name).HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(m => m.Surname).HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(m => m.UserName).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(m => m.NormalizedUserName).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(m => m.Email).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(m => m.NormalizedEmail).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(m => m.EmailConfirmed).HasColumnType("bit").IsRequired();
            builder.Property(m => m.PasswordHash).HasColumnType("varchar").HasMaxLength(400).IsRequired();
            builder.Property(m => m.SecurityStamp).HasColumnType("varchar").HasMaxLength(400).IsRequired();
            builder.Property(m => m.ConcurrencyStamp).HasColumnType("varchar").HasMaxLength(400).IsRequired();
            builder.Property(m => m.PhoneNumber).HasColumnType("varchar").HasMaxLength(40);
            builder.Property(m => m.PhoneNumberConfirmed).HasColumnType("bit").IsRequired();
            builder.Property(m => m.TwoFactorEnabled).HasColumnType("bit").IsRequired();
            builder.Property(m => m.LockoutEnd).HasColumnType("datetimeoffset");
            builder.Property(m => m.LockoutEnabled).HasColumnType("bit").IsRequired();
            builder.Property(m => m.AccessFailedCount).HasColumnType("int").IsRequired();

            builder.Property(m => m.PhoneConfirmationCodeGeneratedAt).HasColumnType("datetime");
            builder.Property(m => m.PasswordResetTokenGeneratedAt).HasColumnType("datetime");
            builder.Property(m => m.PhoneConfirmationCode).HasColumnType("varchar").HasMaxLength(400);
            builder.Property(m => m.PasswordResetToken).HasColumnType("varchar").HasMaxLength(450);
            builder.Property(m => m.ProfileImgUrl).HasColumnType("nvarchar").HasMaxLength(255);

            builder.HasKey(m => m.Id);
            builder.ToTable("Users", "Membership");
        }
    }
}
