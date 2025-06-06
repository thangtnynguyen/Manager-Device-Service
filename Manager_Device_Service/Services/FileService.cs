﻿using Manager_Device_Service.Domains.Model.File;
using Manager_Device_Service.Services.Interfaces;

namespace Manager_Device_Service.Services
{
    public class FileService:IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFileAsync(IFormFile? file, string folder)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string relativeFolderPath = folder;
            string uploadsFolder = Path.Combine(webRootPath, relativeFolderPath);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(relativeFolderPath, uniqueFileName);

            using (var stream = new FileStream(Path.Combine(webRootPath, filePath), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string absoluteFilePath = Path.Combine(webRootPath, folder, uniqueFileName);
            string relativeFilePath = Path.GetRelativePath(webRootPath, absoluteFilePath);

            return "/" + relativeFilePath.Replace("\\", "/");
        }

        public async Task<List<string>> UploadMultipleFilesAsync(UploadMultipleFileRequest request, string folder)
        {
            List<string> uploadedFilePaths = new List<string>();

            foreach (var file in request.Files)
            {
                if (file.Length > 0)
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string relativeFolderPath = folder;
                    string uploadsFolder = Path.Combine(webRootPath, relativeFolderPath);

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" +file.FileName;
                    string filePath = Path.Combine(relativeFolderPath, uniqueFileName);

                    using (var stream = new FileStream(Path.Combine(webRootPath, filePath), FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    string absoluteFilePath = Path.Combine(webRootPath, folder, uniqueFileName);
                    string relativeFilePath = Path.GetRelativePath(webRootPath, absoluteFilePath);

                    uploadedFilePaths.Add("/" + relativeFilePath.Replace("\\", "/"));
                }
            }

            return uploadedFilePaths;
        }



        public async Task<bool> DeleteFileAsync(string fileUrl)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string filePath = Path.Combine(webRootPath, fileUrl.TrimStart('/'));

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<string>> DeleteFilesAsync(List<string> fileUrls)
        {
            List<string> deletedUrls = new List<string>();

            foreach (var fileUrl in fileUrls)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string filePath = Path.Combine(webRootPath, fileUrl.TrimStart('/'));

                if (File.Exists(filePath))
                {
                    await Task.Run(() => File.Delete(filePath));
                    deletedUrls.Add(fileUrl);
                }
            }

            return deletedUrls;
        }
    }

}
