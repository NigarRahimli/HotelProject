using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Infrastructure.Abstracts
{
    public interface IFileService
    {
        Task<IEnumerable<string>> UploadAsync(IEnumerable<IFormFile> files, string subDirectory = "images");
        Task<string> UploadSingleAsync(IFormFile file, string subDirectory = "icons");
        Task<IEnumerable<string>> ChangeFileAsync(IEnumerable<string> oldFileNames, IEnumerable<IFormFile> newFiles, string subDirectory = "images");
        Task<string> ChangeSingleFileAsync(string oldFileName, IFormFile newFile, string subDirectory = "icons");
    }
}
