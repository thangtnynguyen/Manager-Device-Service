

using Manager_Device_Service.Domains.Model.File;

namespace Manager_Device_Service.Services.Interfaces
{
    public interface IFileService
    {

        Task<string> UploadFileAsync(IFormFile? file, string folder);

        Task<bool> DeleteFileAsync(string fileUrl);

        Task<List<string>> DeleteFilesAsync(List<string> fileUrls);
         
        Task<List<string>> UploadMultipleFilesAsync(UploadMultipleFileRequest request, string folder);



    }
}
