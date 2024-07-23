using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.PropertyImagesModule.Commands.PropertyImageEditCommand;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Modules.PropertyImagesModule.Commands.PropertyImageUpdateCommand
{
    public class PropertyImagesEditRequestHandler : IRequestHandler<PropertyImagesEditRequest, IEnumerable<PropertyImage>>
    {
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly IFileService fileService;
        private readonly ILogger<PropertyImagesEditRequestHandler> logger;

        public PropertyImagesEditRequestHandler(
            IFileService fileService,
            IPropertyImageRepository propertyImageRepository,
            ILogger<PropertyImagesEditRequestHandler> logger,
            IPropertyRepository propertyRepository)
        {
            this.fileService = fileService;
            this.propertyImageRepository = propertyImageRepository;
            this.logger = logger;
            this.propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<PropertyImage>> Handle(PropertyImagesEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling PropertyImagesEditRequest for PropertyId {PropertyId}", request.PropertyId);
            var property = await propertyRepository.GetAsync(x => x.Id == request.PropertyId && x.DeletedBy == null, cancellationToken);

            logger.LogInformation("Checked property with PropertyId {PropertyId} exists", request.PropertyId);

            var existingImages = propertyImageRepository.GetAll(x => x.PropertyId == request.PropertyId).ToList();
            var oldFileNames = existingImages.Select(image => image.Image).ToList();

            logger.LogInformation("Found {Count} existing images for PropertyId {PropertyId}", existingImages.Count, request.PropertyId);

            var uploadedFiles = await fileService.ChangeFileAsync(oldFileNames, request.Images);

            foreach (var image in existingImages)
            {
                logger.LogInformation("Editing existing image {Image} for PropertyId {PropertyId}", image.Image, image.PropertyId);
                propertyImageRepository.Edit(image);
            }

            var propertyImages = uploadedFiles.Select(file => new PropertyImage
            {
                PropertyId = request.PropertyId,
                Image = file.FileName,
                Url = file.Url,
            }).ToList();

            foreach (var propertyImage in propertyImages)
            {
                logger.LogInformation("Adding new property image {Image} for PropertyId {PropertyId}", propertyImage.Image, propertyImage.PropertyId);
                await propertyImageRepository.AddAsync(propertyImage, cancellationToken);
            }

            await propertyImageRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Successfully handled PropertyImagesEditRequest for PropertyId {PropertyId}", request.PropertyId);

            return propertyImages;
        }
    }
}
