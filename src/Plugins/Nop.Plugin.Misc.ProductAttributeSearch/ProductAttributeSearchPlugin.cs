using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.ProductAttributeSearch;

public class ProductAttributeSearchPlugin : BasePlugin, IMiscPlugin
{
    private readonly ILocalizationService _localizationService;

    public ProductAttributeSearchPlugin(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public override async Task InstallAsync()
    {
        //locales
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Admin.Catalog.Attributes.ProductAttributes.List.SearchName"] = "Search name",
            ["Admin.Catalog.Attributes.ProductAttributes.List.SearchName.Hint"] = "Enter a product attribute name to filter the list.",
        });

        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _localizationService.DeleteLocaleResourceAsync("Admin.Catalog.Attributes.ProductAttributes.List.SearchName");

        await base.UninstallAsync();
    }
}
