namespace AdviceRequestApp.Services;

public interface IWorkflowService
{
    Task<bool> SubmitWorkflowAsync(Models.FormData formData, string advReqId);
}
