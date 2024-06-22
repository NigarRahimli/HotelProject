using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Project.Infrastructure.Abstracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Resume.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IHostEnvironment env;

        public FileService(IHostEnvironment env)
        {
            this.env = env;
        }

        public async Task<IEnumerable<string>> UploadAsync(IEnumerable<IFormFile> files)
        {
            var uploadedFileNames = new List<string>();

            foreach (var file in files)
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

                uploadedFileNames.Add(randomFileName);
            }

            return uploadedFileNames;
        }

        public async Task<IEnumerable<string>> ChangeFileAsync(IEnumerable<string> oldFileNames, IEnumerable<IFormFile> newFiles)
        {
            var uploadedFileNames = new List<string>();

            foreach (var oldFileName in oldFileNames)
            {
                string oldFilePath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", oldFileName);

                if (File.Exists(oldFilePath))
                {
                    string archiveFilePath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", $"archive-{oldFileName}");

                    File.Move(oldFilePath, archiveFilePath);
                }
            }

            foreach (var file in newFiles)
            {
                var uploadedFileName = await UploadAsync(new List<IFormFile> { file });
                uploadedFileNames.AddRange(uploadedFileName);
            }

            return uploadedFileNames;
        }
    }
}
