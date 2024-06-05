using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Commands.PropertyEditCommand
{
    class PropertyEditRequestHandler : IRequestHandler<PropertyEditRequest, Property>
    {
        private readonly IPropertyRepository PropertyRepository;

        public PropertyEditRequestHandler(IPropertyRepository PropertyRepository)
        {
            this.PropertyRepository = PropertyRepository;
        }
        public async Task<Property> Handle(PropertyEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await PropertyRepository.GetAsync(m=>m.Id==request.Id);

            entity.Name=request.Name;
            entity.KindId=request.KindId;
            entity.DescriptionId=request.DescriptionId;
            entity.GuestNum=request.GuestNum;

            await PropertyRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
