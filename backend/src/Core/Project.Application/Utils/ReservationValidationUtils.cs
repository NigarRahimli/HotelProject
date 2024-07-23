using Project.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Utils
{
    public static class ReservationValidationUtils
    {
        public static bool BeAValidReservationStatus(int status)
        {
            return Enum.IsDefined(typeof(ReservationStatus), status);
        }
    }
}
