using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;
using Resume.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityEditCommand
{
    class AmenityEditRequestHandler : IRequestHandler<AmenityEditRequest, Amenity>
    {
        private readonly IAmenityRepository AmenityRepository;
        private readonly IFileService fileService;

        public AmenityEditRequestHandler(IAmenityRepository AmenityRepository, IFileService fileService)
        {
            this.AmenityRepository = AmenityRepository;
            this.fileService = fileService;
        }
        public async Task<Amenity> Handle(AmenityEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await AmenityRepository.GetAsync(m=>m.Id==request.Id && m.DeletedBy==null);

            entity.Name=request.Name;
            if (request.Image is not null)
            {
              var imageName=await fileService.ChangeSingleFileAsync(entity.IconUrl, request.Image);
            }
            await AmenityRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
