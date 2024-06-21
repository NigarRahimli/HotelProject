using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Project.Infrastructure.Abstracts;

namespace Resume.Application.Services
{
    class FileService : IFileService
    {
        private readonly IHostEnvironment env;

        public FileService(IHostEnvironment env)
        {
            this.env = env;
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName); //.jpg
            string randomFileName = $"{Guid.NewGuid()}{extension}";
            string fullName = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", randomFileName);
            var fileInfo = new FileInfo(fullName);

            if (fileInfo.Directory?.Exists != true)
                fileInfo.Directory?.Create();

            using (var fs = new FileStream(fileInfo.FullName, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fs);
            }

            return randomFileName;
        }

        public Task<string> ChangeFileAsync(string oldFileName, IFormFile file)
        {
            string oldFilePath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", oldFileName);

            if (File.Exists(oldFilePath))
            {
                string archiveFilePath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", $"archive-{oldFileName}");

                File.Move(oldFilePath, archiveFilePath);
            }

            return UploadAsync(file);
        }
    }
}
