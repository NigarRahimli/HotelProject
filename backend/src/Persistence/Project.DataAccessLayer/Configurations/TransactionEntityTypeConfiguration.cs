using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.Entities;
using Transaction = Project.Domain.Models.Entities.Transaction;

namespace Project.DataAccessLayer.Configurations
{
    class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(m => m.TransactionDate).HasColumnType("datetime").IsRequired();
            builder.Property(m => m.Amount).HasColumnType("decimal").IsRequired();
            builder.Property(m => m.TransactionStatus).HasColumnType("int").IsRequired();
            builder.Property(m => m.PaymentMethod).HasColumnType("int").IsRequired();
            

            builder.HasKey(m => m.Id);
            builder.HasOne<Reservation>()
                   .WithMany()
                   .HasPrincipalKey(m => m.Id)
                   .HasForeignKey(m => m.ReservationId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.ConfigureAuditable();

            builder.HasKey(m => m.Id);
            builder.ToTable("Transactions");
        }
    }
}
