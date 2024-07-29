using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Domain.Models.Enums;
using Project.Infrastructure.Exceptions;

namespace Project.Application.Modules.TransactionModule.Commands.ProcessPaymentCommand
{
    public class ProcessPaymentRequestHandler : IRequestHandler<ProcessPaymentRequest, Transaction>
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IReservationRepository reservationRepository;
        private readonly IStripePaymentService stripePaymentService;
        private readonly ILogger<ProcessPaymentRequestHandler> logger;

        public ProcessPaymentRequestHandler(
            ITransactionRepository transactionRepository,
            IStripePaymentService stripePaymentService,
            ILogger<ProcessPaymentRequestHandler> logger,
            IReservationRepository reservationRepository)
        {
            this.transactionRepository = transactionRepository;
            this.stripePaymentService = stripePaymentService;
            this.logger = logger;
            this.reservationRepository = reservationRepository;
        }

        public async Task<Transaction> Handle(ProcessPaymentRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Processing payment for reservation {ReservationId}", request.ReservationId);
            
            await reservationRepository.GetAsync(x => x.Id == request.ReservationId && x.DeletedBy==null);

            bool paymentResult;
            try
            {
                paymentResult = await stripePaymentService.ProcessPayment(request.Amount, "usd", request.Token, $"Payment for reservation {request.ReservationId}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Payment processing failed for reservation {ReservationId}", request.ReservationId);
                throw new OperationFailedException("Payment processing failed.");
            }

            var transaction = new Transaction
            {
                ReservationId = request.ReservationId,
                PaymentMethod = (PaymentMethod) request.PaymentMethod,
                Amount = request.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionStatus = paymentResult ? TransactionStatus.Completed : TransactionStatus.Failed,
            };

            try
            {
                await transactionRepository.AddAsync(transaction, cancellationToken);
                await transactionRepository.SaveAsync(cancellationToken);
                logger.LogInformation("Transaction for reservation {ReservationId} saved successfully", request.ReservationId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Saving transaction failed for reservation {ReservationId}", request.ReservationId);
                throw new OperationFailedException("Saving transaction failed.");
            }

            return transaction;
        }
    }
}
