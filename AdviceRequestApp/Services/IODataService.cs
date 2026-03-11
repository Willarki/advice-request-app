using AdviceRequestApp.Models;

namespace AdviceRequestApp.Services;

public interface IODataService
{
    Task<List<AreaInfo>> GetAreasAsync();
}
