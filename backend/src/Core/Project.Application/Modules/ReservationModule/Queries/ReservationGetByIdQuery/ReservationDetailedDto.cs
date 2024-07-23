using Project.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReservationModule.Queries.ReservationGetByIdQuery
{
    public class ReservationDetailedDto
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string ProfileImgUrl { get; set; }

        // From Property entity
        public int KindId { get; set; }
        public int LocationId { get; set; }
        public string PropertyName { get; set; }

        // From Reservation entity
        public int ReservationId { get; set; }
        public string ReservationName { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public string ReservationStatusName { get; set; }
        public int PropertyId { get; set; }
        public string PaymentOptionName { get; set; }
        public double TotalAmount { get; set; }
    }
}
