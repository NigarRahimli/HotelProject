﻿using MediatR;


namespace Project.Application.Modules.FacilitiesModule.Queries.FacilityGetAllQuery
{
    public class FacilityGetAllRequest:IRequest<IEnumerable<FacilityAllDto>>
    {
    }
}
