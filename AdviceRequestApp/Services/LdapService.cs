using System.DirectoryServices;
using System.Runtime.Versioning;
using AdviceRequestApp.Models;

namespace AdviceRequestApp.Services;

public class LdapService : ILdapService
{
    private readonly IConfigurationService _configService;
    private readonly ILogger<LdapService> _logger;

    public LdapService(IConfigurationService configService, ILogger<LdapService> logger)
    {
        _configService = configService;
        _logger = logger;
    }

    public async Task<LdapUserInfo> GetCurrentUserInfoAsync()
    {
        return await Task.Run(() =>
        {
            try
            {
                if (!OperatingSystem.IsWindows())
                {
                    _logger.LogWarning("LDAP via DirectoryServices is only supported on Windows. Using fallback data.");
                    return GetFallbackUserInfo();
                }
                return QueryLdap();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "LDAP query failed, using fallback demo data.");
                return GetFallbackUserInfo();
            }
        });
    }

    [SupportedOSPlatform("windows")]
    private LdapUserInfo QueryLdap()
    {
        var ldapSettings = _configService.GetLdapSettings();
        string userName = Environment.UserName;
        string ldapPath = $"LDAP://{ldapSettings.Server}/{ldapSettings.BaseDn}";

        using var entry = new DirectoryEntry(ldapPath);
        using var searcher = new DirectorySearcher(entry);

        searcher.Filter = $"(&(objectClass=user)(sAMAccountName={EscapeLdapFilter(userName)}))";
        searcher.PropertiesToLoad.Add("givenName");
        searcher.PropertiesToLoad.Add("sn");
        searcher.PropertiesToLoad.Add("mail");
        searcher.PropertiesToLoad.Add("telephoneNumber");
        searcher.PropertiesToLoad.Add("department");

        var result = searcher.FindOne();

        if (result == null)
        {
            _logger.LogWarning("No LDAP result found for user '{UserName}', using fallback data.", userName);
            return GetFallbackUserInfo();
        }

        return new LdapUserInfo
        {
            FirstName = GetProperty(result, "givenName"),
            LastName = GetProperty(result, "sn"),
            Email = GetProperty(result, "mail"),
            PhoneNumber = GetProperty(result, "telephoneNumber"),
            Department = GetProperty(result, "department")
        };
    }

    [SupportedOSPlatform("windows")]
    private static string GetProperty(SearchResult result, string propertyName)
    {
        if (result.Properties.Contains(propertyName) && result.Properties[propertyName].Count > 0)
        {
            return result.Properties[propertyName][0]?.ToString() ?? string.Empty;
        }
        return string.Empty;
    }

    private static string EscapeLdapFilter(string value)
    {
        return value
            .Replace("\\", "\\5c")
            .Replace("*", "\\2a")
            .Replace("(", "\\28")
            .Replace(")", "\\29")
            .Replace("\0", "\\00");
    }

    private static LdapUserInfo GetFallbackUserInfo()
    {
        string userName = Environment.UserName;
        return new LdapUserInfo
        {
            FirstName = userName,
            LastName = string.Empty,
            Email = $"{userName}@domain.com",
            PhoneNumber = string.Empty,
            Department = string.Empty
        };
    }
}
