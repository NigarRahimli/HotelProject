using MediatR;
using Project.Application.Modules.PropertyImagesModule.Commands.PropertyImageEditCommand;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;


namespace Project.Application.Modules.PropertyImagesModule.Commands.PropertyImageUpdateCommand
{
    public class PropertyImageEditRequestHandler : IRequestHandler<PropertyImageEditRequest, IEnumerable<PropertyImage>>
    {
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IFileService fileService;

        public PropertyImageEditRequestHandler(IFileService fileService, IPropertyImageRepository propertyImageRepository)
        {
            this.fileService = fileService;
            this.propertyImageRepository = propertyImageRepository;
        }

        public async Task<IEnumerable<PropertyImage>> Handle(PropertyImageEditRequest request, CancellationToken cancellationToken)
        {
            // Step 1: Mark existing images as deleted
            var existingImages = propertyImageRepository.GetAll(x=>x.PropertyId== request.PropertyId);

            foreach (var image in existingImages)
            {

                image.DeletedBy = request.UserId;
                image.DeletedAt = DateTime.UtcNow;
                 propertyImageRepository.Edit(image);
            }

           
            var uploadedFileNames = await fileService.UploadAsync(request.Images);
            var propertyImages = uploadedFileNames.Select(fileName => new PropertyImage
            {
                PropertyId = request.PropertyId,
                Image = fileName,
                Url = $"/uploads/images/{fileName}",
                CreatedBy = request.UserId,
                CreatedAt = DateTime.UtcNow
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
