using System.Net.Http.Json;
using AdviceRequestApp.Models;

namespace AdviceRequestApp.Services;

public class WorkflowService : IWorkflowService
{
    private readonly HttpClient _httpClient;
    private readonly IConfigurationService _configService;
    private readonly ILogger<WorkflowService> _logger;

    public WorkflowService(HttpClient httpClient, IConfigurationService configService, ILogger<WorkflowService> logger)
    {
        _httpClient = httpClient;
        _configService = configService;
        _logger = logger;
    }

    public async Task<bool> SubmitWorkflowAsync(FormData formData, string advReqId)
    {
        try
        {
            var endpoints = _configService.GetApiEndpoints();
            var request = new WorkflowSubmitRequest
            {
                Folio = "Test Instance",
                DataFields = new WorkflowDataFields
                {
                    P_ResponsibleArea = formData.ResponsibleAreaName,
                    P_RequestedTimeframe = formData.RequiredByDate.HasValue
                        ? formData.RequiredByDate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")
                        : string.Empty,
                    P_TimeframeRationale = formData.ReasonForExpediting,
                    P_RequestingOfficerFName = formData.FirstName,
                    P_RequestingOfficerLName = formData.LastName,
                    P_RequestingArea = formData.BusinessArea,
                    P_RequestingOfficerPhone = formData.PhoneNumber,
                    P_RequestingOfficerEmail = formData.Email,
                    P_IsCustomer = formData.IsCustomer,
                    P_CustID = formData.IsCustomer ? formData.CustID : string.Empty,
                    P_AdviceDescription = formData.RequestDescription,
                    P_AdviceDetails = formData.RequestDetails,
                    ReqID = advReqId
                }
            };

            var response = await _httpClient.PostAsJsonAsync(endpoints.WorkflowSubmitUrl, request);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to submit workflow.");
            return false;
        }
    }
}
