using Project.Domain.Models.Enums;

namespace Project.Application.Utils
{
    public static class TransactionValidationUtils
    {
        public static bool BeAValidTransactionMethod(int paymentMethod)
        {
            return Enum.IsDefined(typeof(PaymentMethod), paymentMethod);
        }
    }
}
