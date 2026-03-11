using AdviceRequestApp.Models;

namespace AdviceRequestApp.Services;

public interface ILdapService
{
    Task<LdapUserInfo> GetCurrentUserInfoAsync();
}
