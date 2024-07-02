using Microsoft.Extensions.Options;
using Project.Infrastructure.Common;
using Stripe;
using System.Threading.Tasks;

public class StripePaymentService : IStripePaymentService
{
    private readonly StripeServiceOptions serviceOptions;

    public StripePaymentService(IOptions<StripeServiceOptions> stripeOptions)
    {
        serviceOptions = stripeOptions.Value;
        StripeConfiguration.ApiKey = serviceOptions.SecretKey;
    }

    public async Task<bool> ProcessPayment(decimal amount, string currency, string source, string description)
    {
        var options = new ChargeCreateOptions
        {
            Amount = (long)(amount * 100), // Convert amount to cents
            Currency = currency,
            Source = source,
            Description = description,
        };

        var service = new ChargeService();
        Charge charge = await service.CreateAsync(options);

        return charge.Status == "succeeded";
    }
}
