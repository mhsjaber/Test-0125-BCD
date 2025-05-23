using Nop.Plugin.Widgets.CheckoutGiftMessage.Components;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage;

public class CheckoutGiftMessagePlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;

    public CheckoutGiftMessagePlugin(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public bool HideInWidgetList => false;

    public Type GetWidgetViewComponent(string widgetZone)
    {
        if (widgetZone == PublicWidgetZones.OrderDetailsPageOverview)
            return typeof(PublicOrderDetailsViewComponent);

        return typeof(ShoppingCartViewComponent);
    }

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string>()
        { 
            PublicWidgetZones.OrderDetailsPageOverview,
            PublicWidgetZones.OrderSummaryContentDeals
        });
    }

    public override async Task InstallAsync()
    {
        //locales
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["ShoppingCart.GiftMessage"] = "Gift Message",
            ["ShoppingCart.GiftMessage.Tooltip"] = "Enter gift message",
            ["ShoppingCart.GiftMessage.Label"] = "Enter gift message",
            ["ShoppingCart.GiftMessage.Saved"] = "Gift message saved",
            ["ShoppingCart.GiftMessage.Button"] = "Add Message",
            ["ShoppingCart.GiftMessage.Required"] = "Gift message cannot be empty",
            ["ShoppingCart.GiftMessage.MaxLength"] = "Gift message maximum length is {0}",
            ["Order.GiftMessage"] = "Gift Message",
            ["Admin.Orders.Fields.GiftMessage"] = "Gift message",
            ["Admin.Orders.Fields.GiftMessage.Hint"] = "The order gift message.",
        });

        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _localizationService.DeleteLocaleResourceAsync("Admin.Catalog.Attributes.ProductAttributes.List.SearchName");

        await base.UninstallAsync();
    }
}
