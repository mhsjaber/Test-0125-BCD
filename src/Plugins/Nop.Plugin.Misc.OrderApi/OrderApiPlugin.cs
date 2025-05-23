using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.OrderApi;

public class OrderApiPlugin : BasePlugin, IMiscPlugin
{
    private readonly ILocalizationService _localizationService;

    public OrderApiPlugin(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public override async Task InstallAsync()
    {
        await _localizationService.AddOrUpdateLocaleResourceAsync(StringResources);

        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _localizationService.DeleteLocaleResourcesAsync(StringResources.Keys.ToList());

        await base.UninstallAsync();
    }

    protected IDictionary<string, string> StringResources => new Dictionary<string, string>
    {
        ["Account.Login.WrongCredentials.MultiFactorAuthenticationRequired"] = "Multi factor authentication is required.",
    };
}
