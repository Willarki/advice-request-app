using System.Text.Json.Serialization;

namespace AdviceRequestApp.Models;

public class FileUploadRequest
{
    [JsonPropertyName("DocID")]
    public int DocID { get; set; } = 610;

    [JsonPropertyName("FileName")]
    public string FileName { get; set; } = string.Empty;

    [JsonPropertyName("ReqID")]
    public string ReqID { get; set; } = string.Empty;

    [JsonPropertyName("ElecFile")]
    public string ElecFile { get; set; } = string.Empty;
}

public class FileUploadResponse
{
    [JsonPropertyName("FileID")]
    public int FileID { get; set; }
}

public class FileDeleteRequest
{
    [JsonPropertyName("FileID")]
    public int FileID { get; set; }
}

public class WorkflowSubmitRequest
{
    [JsonPropertyName("folio")]
    public string Folio { get; set; } = string.Empty;

    [JsonPropertyName("dataFields")]
    public WorkflowDataFields DataFields { get; set; } = new();
}

public class WorkflowDataFields
{
    [JsonPropertyName("P_ResponsibleArea")]
    public string P_ResponsibleArea { get; set; } = string.Empty;

    [JsonPropertyName("P_RequestedTimeframe")]
    public string P_RequestedTimeframe { get; set; } = string.Empty;

    [JsonPropertyName("P_TimeframeRationale")]
    public string P_TimeframeRationale { get; set; } = string.Empty;

    [JsonPropertyName("P_RequestingOfficerFName")]
    public string P_RequestingOfficerFName { get; set; } = string.Empty;

    [JsonPropertyName("P_RequestingOfficerLName")]
    public string P_RequestingOfficerLName { get; set; } = string.Empty;

    [JsonPropertyName("P_RequestingArea")]
    public string P_RequestingArea { get; set; } = string.Empty;

    [JsonPropertyName("P_RequestingOfficerPhone")]
    public string P_RequestingOfficerPhone { get; set; } = string.Empty;

    [JsonPropertyName("P_RequestingOfficerEmail")]
    public string P_RequestingOfficerEmail { get; set; } = string.Empty;

    [JsonPropertyName("P_IsCustomer")]
    public bool P_IsCustomer { get; set; }

    [JsonPropertyName("P_CustID")]
    public string P_CustID { get; set; } = string.Empty;

    [JsonPropertyName("P_AdviceDescription")]
    public string P_AdviceDescription { get; set; } = string.Empty;

    [JsonPropertyName("P_AdviceDetails")]
    public string P_AdviceDetails { get; set; } = string.Empty;

    [JsonPropertyName("ReqID")]
    public string ReqID { get; set; } = string.Empty;
}
