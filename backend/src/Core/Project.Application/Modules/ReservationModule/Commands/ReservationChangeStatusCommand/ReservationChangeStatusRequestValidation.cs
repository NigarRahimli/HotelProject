using FluentValidation;
using Project.Application.Modules.ReservationModule.Commands.ReservationAddCommand;
using Project.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReservationModule.Commands.ReservationChangeStatusCommand
{
    public class ReservationChangeStatusRequestValidation : AbstractValidator<ReservationChangeStatusRequest>
    {
        public ReservationChangeStatusRequestValidation() {
            RuleFor(x => x.ReservationStatus)
        .Must(ReservationValidationUtils.BeAValidReservationStatus).WithErrorCode("INVALID_RESERVATION_STATUS");
        }

    }
}
