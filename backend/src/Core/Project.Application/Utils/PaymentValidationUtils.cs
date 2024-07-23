using Project.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Utils
{
    public static class PaymentValidationUtils
    {
        public static bool BeAValidPaymentOption(int paymentOption)
        {
            return Enum.IsDefined(typeof(PaymentOption), paymentOption);
        }
    }
}
