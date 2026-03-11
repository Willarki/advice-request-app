using AdviceRequestApp.Models;

namespace AdviceRequestApp.Services;

public interface IFileService
{
    Task<int> UploadFileAsync(string fileName, string base64Content, string reqId);
    Task DeleteFileAsync(int fileId);
}
