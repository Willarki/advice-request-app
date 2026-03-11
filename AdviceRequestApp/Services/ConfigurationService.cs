using AdviceRequestApp.Models;

namespace AdviceRequestApp.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ApiEndpoints GetApiEndpoints()
    {
        var endpoints = new ApiEndpoints();
        _configuration.GetSection("ApiEndpoints").Bind(endpoints);
        return endpoints;
    }

    public LdapSettings GetLdapSettings()
    {
        var ldap = new LdapSettings();
        _configuration.GetSection("LdapSettings").Bind(ldap);
        return ldap;
    }

    public ApplicationSettings GetAppSettings()
    {
        var settings = new ApplicationSettings();
        _configuration.GetSection("AppSettings").Bind(settings);
        return settings;
    }
}
