using Microsoft.AspNetCore.Components.Forms;

namespace AdviceRequestApp.Models;

public class FormData
{
    // Page 1: Requester Information
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string BusinessArea { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    // Page 2: Request Details
    public int? ResponsibleAreaId { get; set; }
    public string ResponsibleAreaName { get; set; } = string.Empty;
    public string RequestDescription { get; set; } = string.Empty;
    public string RequestDetails { get; set; } = string.Empty;
    public bool IsCustomer { get; set; }
    public string CustID { get; set; } = string.Empty;

    // Page 3: Finalize Request
    public DateTime? RequiredByDate { get; set; }
    public string ReasonForExpediting { get; set; } = string.Empty;
    public List<UploadedFile> UploadedFiles { get; set; } = new();
}

public class UploadedFile
{
    public string FileName { get; set; } = string.Empty;
    public long Size { get; set; }
    public int FileID { get; set; }
    public string Base64Content { get; set; } = string.Empty;

    public string FormattedSize
    {
        get
        {
            if (Size < 1024)
            {
                return $"{Size} B";
            }
            if (Size < 1024 * 1024)
            {
                return $"{Size / 1024.0:F1} KB";
            }
            return $"{Size / (1024.0 * 1024.0):F1} MB";
        }
    }
}
