using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AdviceRequestApp.Models;

namespace AdviceRequestApp.Services;

public class FileService : IFileService
{
    private readonly HttpClient _httpClient;
    private readonly IConfigurationService _configService;
    private readonly ILogger<FileService> _logger;

    public FileService(HttpClient httpClient, IConfigurationService configService, ILogger<FileService> logger)
    {
        _httpClient = httpClient;
        _configService = configService;
        _logger = logger;
    }

    public async Task<int> UploadFileAsync(string fileName, string base64Content, string reqId)
    {
        var endpoints = _configService.GetApiEndpoints();
        var request = new FileUploadRequest
        {
            DocID = 610,
            FileName = fileName,
            ReqID = reqId,
            ElecFile = $"<file>{base64Content}</file>"
        };

        var response = await _httpClient.PutAsJsonAsync(endpoints.FileUploadUrl, request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var result = JsonSerializer.Deserialize<FileUploadResponse>(json, options);

        return result?.FileID ?? 0;
    }

    public async Task DeleteFileAsync(int fileId)
    {
        try
        {
            var endpoints = _configService.GetApiEndpoints();
            var request = new FileDeleteRequest { FileID = fileId };
            var content = System.Net.Http.Json.JsonContent.Create(request);
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, endpoints.FileDeleteUrl)
            {
                Content = content
            };
            var response = await _httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete file with ID {FileID}.", fileId);
        }
    }
}
