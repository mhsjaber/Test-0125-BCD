using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.CheckoutGiftMessage.Models;
using Nop.Services.Common;
using Nop.Services.Html;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Components;

public class ShoppingCartViewComponent : NopViewComponent
{
    #region Fields

    private readonly IGenericAttributeService _genericAttributeService;
    private readonly IWorkContext _workContext;
    private readonly IStoreContext _storeContext;

    #endregion

    #region Ctor

    public ShoppingCartViewComponent(IGenericAttributeService genericAttributeService,
        IWorkContext workContext,
        IStoreContext storeContext)
    {
        _genericAttributeService = genericAttributeService;
        _workContext = workContext;
        _storeContext = storeContext;
    }

    #endregion

    #region Methods

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var customer = await _workContext.GetCurrentCustomerAsync();
        var store = await _storeContext.GetCurrentStoreAsync();

        var model = new ShoppingCartGiftMessageModel();
        model.GiftMessage = await _genericAttributeService.GetAttributeAsync<string>(customer, CheckoutGiftMessageDefaults.GiftMessageAttributeName, store.Id);

        return View("~/Plugins/Widgets.CheckoutGiftMessage/Views/ShoppingCart.cshtml", model);
    }

    #endregion
}
