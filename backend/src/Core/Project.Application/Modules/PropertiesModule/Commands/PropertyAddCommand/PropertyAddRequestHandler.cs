using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Exceptions;
using Project.Infrastructure.Extensions;

namespace Project.Application.Modules.PropertiesModule.Commands.PropertyAddCommand
{
    class PropertyAddRequestHandler : IRequestHandler<PropertyAddRequest, Property>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IKindRepository kindRepository;
        private readonly IDescriptionRepository descriptionRepository;
        private readonly IHttpContextAccessor httpContextAccessor;


        public PropertyAddRequestHandler(IPropertyRepository propertyRepository, ILocationRepository locationRepository, IKindRepository kindRepository, IDescriptionRepository descriptionRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.propertyRepository = propertyRepository;
            this.locationRepository = locationRepository;
            this.kindRepository = kindRepository;
            this.descriptionRepository = descriptionRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Property> Handle(PropertyAddRequest request, CancellationToken cancellationToken)
        {

            var description = await descriptionRepository.GetAsync(m => m.Id == request.DescriptionId, cancellationToken);
            if (description == null)
            {
                throw new NotFoundException($"{nameof(Description)} with {request.DescriptionId} not found");
            }

            var kind = await kindRepository.GetAsync(m => m.Id == request.KindId, cancellationToken);
            if (kind == null)
            {
                throw new NotFoundException($"{nameof(Kind)} with {request.KindId} not found");
            }
            var location = await locationRepository.GetAsync(m => m.Id == request.KindId, cancellationToken);
            if (location == null)
            {
                throw new NotFoundException($"{nameof(Location)} with {request.LocationId} not found");
            }
            var userId=httpContextAccessor.HttpContext.GetUserIdExtension();
            var property = new Property
            {
                Name = request.Name,
                DescriptionId = request.DescriptionId,
                GuestNum=request.GuestNum,
                KindId= request.KindId,
                CreatedAt = DateTime.UtcNow,
                LocationId = request.LocationId,
                IsPetFriendly=request.IsPetFriendly,
                LongPrice=request.LongPrice,
                MedPrice=request.MedPrice,
                MinPrice=request.MinPrice,
                CreatedBy = userId
            };
            await propertyRepository.AddAsync(property, cancellationToken);
            await propertyRepository.SaveAsync(cancellationToken);

            return property;
        }
    }
}
