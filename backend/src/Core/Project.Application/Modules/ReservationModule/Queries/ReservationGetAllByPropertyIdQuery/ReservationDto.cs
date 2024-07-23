using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReservationModule.Queries.ReservationGetAllByPropertyIdQuery
{
    public class ReservationDto
    {
        public string Name { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public string ReservationStatusName { get; set; }
    }
}
