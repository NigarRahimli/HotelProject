using Project.Domain.Models.Enums;
using Project.Infrastructure.Concretes;


namespace Project.Domain.Models.Entities
{
    public class Transaction : AuditableEntity
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
    }
}
