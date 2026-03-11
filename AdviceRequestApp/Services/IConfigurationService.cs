using AdviceRequestApp.Models;

namespace AdviceRequestApp.Services;

public interface IConfigurationService
{
    ApiEndpoints GetApiEndpoints();
    LdapSettings GetLdapSettings();
    ApplicationSettings GetAppSettings();
}
