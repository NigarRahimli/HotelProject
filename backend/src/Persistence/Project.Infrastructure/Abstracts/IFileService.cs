using Microsoft.AspNetCore.Http;

namespace Project.Infrastructure.Abstracts
{
    public interface IFileService
    {
        Task<IEnumerable<(string FileName, string Url)>> UploadAsync(IEnumerable<IFormFile> files, string subDirectory = "images");
        Task<(string FileName, string Url)> UploadSingleAsync(IFormFile file, string subDirectory = "icons");
        Task<IEnumerable<(string FileName, string Url)>> ChangeFileAsync(IEnumerable<string> oldFileNames, IEnumerable<IFormFile> newFiles, string subDirectory = "images");
        Task<(string FileName, string Url)> ChangeSingleFileAsync(string oldFileName, IFormFile newFile, string subDirectory = "icons");
    }
}
