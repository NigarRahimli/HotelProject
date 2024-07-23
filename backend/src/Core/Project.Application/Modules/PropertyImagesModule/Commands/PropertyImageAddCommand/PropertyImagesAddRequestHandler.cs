using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Exceptions;


namespace Project.Application.Modules.PropertyImagesModule.Commands.PropertyImageAddCommand
{
    public class PropertyImagesAddRequestHandler : IRequestHandler<PropertyImagesAddRequest, IEnumerable<PropertyImage>>
    {
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly IFileService fileService;
        private readonly ILogger<PropertyImagesAddRequestHandler> logger;

        public PropertyImagesAddRequestHandler(
            IFileService fileService,
            IPropertyImageRepository propertyImageRepository,
            ILogger<PropertyImagesAddRequestHandler> logger,
            IPropertyRepository propertyRepository)
        {
            this.fileService = fileService;
            this.propertyImageRepository = propertyImageRepository;
            this.logger = logger;
            this.propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<PropertyImage>> Handle(PropertyImagesAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling PropertyImagesAddRequest for PropertyId {PropertyId}", request.PropertyId);
            var property= await propertyRepository.GetAsync(x=>x.Id==request.PropertyId && x.DeletedBy==null, cancellationToken);
         
            logger.LogInformation("Checked property with PropertyId {PropertyId} exists", request.PropertyId);
            var uploadedFiles = await fileService.UploadAsync(request.Images);
           
            var propertyImages = uploadedFiles.Select(file => new PropertyImage
            {
                PropertyId = request.PropertyId,
                Image = file.FileName,
                Url = file.Url,
            }).ToList();

            foreach (var propertyImage in propertyImages)
            {
                logger.LogInformation("Adding property image {Image} for PropertyId {PropertyId}", propertyImage.Image, propertyImage.PropertyId);
                await propertyImageRepository.AddAsync(propertyImage, cancellationToken);
            }

            await propertyImageRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Successfully handled PropertyImagesAddRequest for PropertyId {PropertyId}", request.PropertyId);

            return propertyImages;
        }
    }
}
