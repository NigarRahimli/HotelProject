using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Project.Infrastructure.Abstracts;

namespace Resume.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IHostEnvironment env;

        public FileService(IHostEnvironment env)
        {
            this.env = env;
        }

        public async Task<IEnumerable<(string FileName, string Url)>> UploadAsync(IEnumerable<IFormFile> files, string subDirectory = "images")
        {
            var uploadedFiles = new List<(string FileName, string Url)>();

            foreach (var file in files)
            {
                string extension = Path.GetExtension(file.FileName); //.jpg
                string randomFileName = $"{Guid.NewGuid()}{extension}";
                string fullName = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", subDirectory, randomFileName);
                var fileInfo = new FileInfo(fullName);

                if (fileInfo.Directory?.Exists != true)
                    fileInfo.Directory?.Create();

                using (var fs = new FileStream(fileInfo.FullName, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fs);
                }

                string fileUrl = $"/uploads/{subDirectory}/{randomFileName}";
                uploadedFiles.Add((randomFileName, fileUrl));
            }

            return uploadedFiles;
        }

        public async Task<(string FileName, string Url)> UploadSingleAsync(IFormFile file, string subDirectory = "icons")
        {
            var uploadedFiles = await UploadAsync(new List<IFormFile> { file }, subDirectory);
            return uploadedFiles.FirstOrDefault();
        }

        public async Task<IEnumerable<(string FileName, string Url)>> ChangeFileAsync(IEnumerable<string> oldFileNames, IEnumerable<IFormFile> newFiles, string subDirectory = "images")
        {
            var uploadedFiles = new List<(string FileName, string Url)>();

            foreach (var oldFileName in oldFileNames)
            {
                string oldFilePath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", subDirectory, oldFileName);

                if (File.Exists(oldFilePath))
                {
                    string archiveFilePath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", subDirectory, $"archive-{oldFileName}");

                    File.Move(oldFilePath, archiveFilePath);
                }
            }

            foreach (var file in newFiles)
            {
                var uploadedFile = await UploadAsync(new List<IFormFile> { file }, subDirectory);
                uploadedFiles.AddRange(uploadedFile);
            }

            return uploadedFiles;
        }

        public async Task<(string FileName, string Url)> ChangeSingleFileAsync(string oldFileName, IFormFile newFile, string subDirectory = "icons")
        {
            var uploadedFiles = await ChangeFileAsync(new List<string> { oldFileName }, new List<IFormFile> { newFile }, subDirectory);
            return uploadedFiles.FirstOrDefault();
        }
    }
}
