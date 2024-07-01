using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Modules.PropertyImagesModule.Commands.PropertyImageAddCommand
{
    public class PropertyImagesAddRequestHandler : IRequestHandler<PropertyImagesAddRequest, IEnumerable<PropertyImage>>
    {
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IFileService fileService;

        public PropertyImagesAddRequestHandler(IFileService fileService, IPropertyImageRepository propertyImageRepository)
        {

            this.fileService = fileService;
            this.propertyImageRepository = propertyImageRepository;
        }

        public async Task<IEnumerable<PropertyImage>> Handle(PropertyImagesAddRequest request, CancellationToken cancellationToken)
        {
            var uploadedFiles = await fileService.UploadAsync(request.Images);
            var propertyImages = uploadedFiles.Select(file => new PropertyImage
            {
                PropertyId = request.PropertyId,
                Image = file.FileName,
                Url = file.Url,
            }).ToList();

            foreach (var propertyImage in propertyImages)
            {
                await propertyImageRepository.AddAsync(propertyImage, cancellationToken);
            }

            await propertyImageRepository.SaveAsync(cancellationToken);

            return propertyImages;
        }
    }
}
