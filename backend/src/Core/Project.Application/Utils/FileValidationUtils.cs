﻿using Microsoft.AspNetCore.Http;

namespace Project.Application.Utils
{
    public static class FileValidationUtils
    {
        public static bool BeAValidImage(IFormFile file)
        {
            if (file == null)
            {
                return false;
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = System.IO.Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(extension);
        }
    }
}
