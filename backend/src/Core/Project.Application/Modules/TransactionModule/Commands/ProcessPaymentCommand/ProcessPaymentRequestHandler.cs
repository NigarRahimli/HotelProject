using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Enums;

namespace Project.Application.Modules.TransactionModule.Commands.ProcessPaymentCommand
{
    public class ProcessPaymentRequestHandler : IRequestHandler<ProcessPaymentRequest, Transaction>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IStripePaymentService stripePaymentService;

        public ProcessPaymentRequestHandler(ITransactionRepository transactionRepository, IStripePaymentService stripePaymentService)
        {
            this.transactionRepository = transactionRepository;
            this.stripePaymentService = stripePaymentService;
        }

        public async Task<Transaction> Handle(ProcessPaymentRequest request, CancellationToken cancellationToken)
        {
            var paymentResult = await stripePaymentService.ProcessPayment(request.Amount, "usd", request.Token, $"Payment for reservation {request.ReservationId}");

            var transaction = new Transaction
            {
                ReservationId = request.ReservationId,
                Amount = request.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionStatus = paymentResult ? TransactionStatus.Completed : TransactionStatus.Failed,
            };

            await transactionRepository.AddAsync(transaction, cancellationToken);
            await transactionRepository.SaveAsync(cancellationToken);
            return transaction;
        }
    }
}