using System.Text.Json;
using AdviceRequestApp.Models;

namespace AdviceRequestApp.Services;

public class ODataService : IODataService
{
    private readonly HttpClient _httpClient;
    private readonly IConfigurationService _configService;
    private readonly ILogger<ODataService> _logger;

    public ODataService(HttpClient httpClient, IConfigurationService configService, ILogger<ODataService> logger)
    {
        _httpClient = httpClient;
        _configService = configService;
        _logger = logger;
    }

    public async Task<List<AreaInfo>> GetAreasAsync()
    {
        try
        {
            var endpoints = _configService.GetApiEndpoints();
            var response = await _httpClient.GetAsync(endpoints.ODataAreasUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<ODataAreasResponse>(json, options);

            return result?.Value ?? new List<AreaInfo>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve areas from OData service.");
            return new List<AreaInfo>();
        }
    }
}
