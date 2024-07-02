using Stripe;

public interface IStripePaymentService
{
    Task<bool> ProcessPayment(decimal amount, string currency, string source, string description);
}
