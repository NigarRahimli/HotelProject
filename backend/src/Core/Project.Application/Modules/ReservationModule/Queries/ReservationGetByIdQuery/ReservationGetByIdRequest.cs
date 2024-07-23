using MediatR;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReservationModule.Queries.ReservationGetByIdQuery
{
    public class ReservationGetByIdRequest : IRequest<ReservationDetailedDto>
    {
        public int Id { get; set; }
    }
}
