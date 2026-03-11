using System.Text.Json.Serialization;

namespace AdviceRequestApp.Models;

public class ODataAreasResponse
{
    [JsonPropertyName("@odata.context")]
    public string? Context { get; set; }

    [JsonPropertyName("@odata.count")]
    public int Count { get; set; }

    [JsonPropertyName("value")]
    public List<AreaInfo> Value { get; set; } = new();
}

public class AreaInfo
{
    [JsonPropertyName("ListValueID")]
    public int ListValueID { get; set; }

    [JsonPropertyName("Area")]
    public string Area { get; set; } = string.Empty;

    [JsonPropertyName("Email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("STypeID")]
    public string STypeID { get; set; } = string.Empty;

    [JsonPropertyName("AdvDays")]
    public string AdvDays { get; set; } = "21";

    public int GetAdvDays()
    {
        if (int.TryParse(AdvDays, out int days))
        {
            return days;
        }
        return 21;
    }
}
