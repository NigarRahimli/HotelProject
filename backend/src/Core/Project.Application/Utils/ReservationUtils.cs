using Project.Domain.Models.Entities;
using System;

namespace Project.Application.Utils
{
    public static class ReservationUtils
    {
        public static double CalculateTotalAmount(DateTime checkInTime, DateTime checkOutTime, Property property)
        {
            var duration = checkOutTime - checkInTime;
            double totalAmount;

            if (duration.TotalDays <= 7)
            {
                totalAmount = property.MinPrice * duration.TotalDays;
            }
            else if (duration.TotalDays <= 30)
            {
                totalAmount = property.MedPrice * duration.TotalDays;
            }
            else
            {
                totalAmount = property.LongPrice * duration.TotalDays;
            }

            return Math.Round(totalAmount, 2);
        }
    }
}
