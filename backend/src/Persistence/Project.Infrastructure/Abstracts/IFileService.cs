using Microsoft.AspNetCore.Http;

namespace Project.Infrastructure.Abstracts
{
    public interface IFileService
    {
        Task<IEnumerable<string>> UploadAsync(IEnumerable<IFormFile> files);
        Task<IEnumerable<string>> ChangeFileAsync(IEnumerable<string> oldFileNames, IEnumerable<IFormFile> newFiles);
    }
}
