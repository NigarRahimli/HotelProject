using MediatR;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.TransactionModule.Commands.ProcessPaymentCommand
{
    public class ProcessPaymentRequest : IRequest<Transaction>
    {
        public int ReservationId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMethod { get; set; }
        public string Token { get; set; }
    }
}
