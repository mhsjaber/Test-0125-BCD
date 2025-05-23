using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Controllers;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Controllers;

public class GiftMessageController : BasePublicController
{
    private readonly INotificationService _notificationService;
    private readonly ILocalizationService _localizationService;
    private readonly IGenericAttributeService _genericAttributeService;
    private readonly IWorkContext _workContext;
    private readonly IStoreContext _storeContext;

    public GiftMessageController(INotificationService notificationService,
        ILocalizationService localizationService,
        IGenericAttributeService genericAttributeService,
        IWorkContext workContext,
        IStoreContext storeContext)
    {
        _notificationService = notificationService;
        _localizationService = localizationService;
        _genericAttributeService = genericAttributeService;
        _workContext = workContext;
        _storeContext = storeContext;
    }

    public async Task<IActionResult> Save(string giftMessage)
    {
        if (string.IsNullOrEmpty(giftMessage))
        {
            _notificationService.ErrorNotification(await _localizationService.GetResourceAsync("ShoppingCart.GiftMessage.Required"));
            return RedirectToRoute("ShoppingCart");
        }

        if (giftMessage.Length > CheckoutGiftMessageDefaults.GiftMessageMaxLength)
        {
            _notificationService.ErrorNotification(string.Format(await _localizationService.GetResourceAsync("ShoppingCart.GiftMessage.MaxLength"), CheckoutGiftMessageDefaults.GiftMessageMaxLength));
            return RedirectToRoute("ShoppingCart");
        }

        var customer = await _workContext.GetCurrentCustomerAsync();
        var store = await _storeContext.GetCurrentStoreAsync();
        await _genericAttributeService.SaveAttributeAsync(customer, CheckoutGiftMessageDefaults.GiftMessageAttributeName, giftMessage, store.Id);

        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("ShoppingCart.GiftMessage.Saved"));
        return RedirectToRoute("ShoppingCart");
    }
}
