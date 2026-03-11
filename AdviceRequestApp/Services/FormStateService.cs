using AdviceRequestApp.Models;

namespace AdviceRequestApp.Services;

public class FormStateService
{
    public string AdvReqID { get; private set; }
    public FormData FormData { get; set; } = new();
    public List<AreaInfo> Areas { get; set; } = new();
    public ApplicationSettings Settings { get; set; } = new();
    public DateTime DefaultRequiredByDate { get; private set; }

    public FormStateService()
    {
        string gid = Guid.NewGuid().ToString();
        AdvReqID = string.Format("{0:X12}", gid.GetHashCode());
    }

    public void SetDefaultRequiredByDate(AreaInfo? selectedArea)
    {
        int advDays = selectedArea?.GetAdvDays() ?? 21;
        DefaultRequiredByDate = DateTime.Today.AddDays(advDays);
        if (!FormData.RequiredByDate.HasValue)
        {
            FormData.RequiredByDate = DefaultRequiredByDate;
        }
    }

    public bool IsExpeditingReasonRequired()
    {
        if (!Settings.SLAProcessing)
        {
            return true;
        }
        if (FormData.RequiredByDate.HasValue && FormData.RequiredByDate.Value.Date < DefaultRequiredByDate.Date)
        {
            return true;
        }
        return false;
    }

    public void Reset()
    {
        FormData = new FormData();
        string gid = Guid.NewGuid().ToString();
        AdvReqID = string.Format("{0:X12}", gid.GetHashCode());
        DefaultRequiredByDate = default;
    }
}
