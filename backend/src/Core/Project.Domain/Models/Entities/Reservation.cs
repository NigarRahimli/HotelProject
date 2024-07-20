using Project.Domain.Models.Enums;
using Project.Infrastructure.Concretes;

namespace Project.Domain.Models.Entities
{
    public class Reservation : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public bool IsApproved { get; set; }
        public int PropertyId { get; set; } 
        public ReservationStatus ReservationStatus { get; set; }
        public PaymentOption PaymentOption { get; set; }
    }
}
