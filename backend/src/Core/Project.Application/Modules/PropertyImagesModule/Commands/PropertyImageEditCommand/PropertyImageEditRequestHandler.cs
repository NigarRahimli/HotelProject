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

            var existingImages = propertyImageRepository.GetAll(x => x.PropertyId == request.PropertyId).ToList();

 
            var oldFileNames = existingImages.Select(image => image.Image).ToList();


            var uploadedFileNames = await fileService.ChangeFileAsync(oldFileNames, request.Images);
           
            foreach (var image in existingImages)
            {
                propertyImageRepository.Edit(image); 
            }

            var propertyImages = uploadedFileNames.Select(fileName => new PropertyImage
            {
                PropertyId = request.PropertyId,
                Image = fileName,
                Url = $"/uploads/images/{fileName}",
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
