using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Widgets.CheckoutGiftMessage.Models;
using Nop.Services.Common;
using Nop.Services.Html;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Order;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Components;

public class PublicOrderDetailsViewComponent : NopViewComponent
{
    #region Fields

    private readonly IHtmlFormatter _htmlFormatter;
    private readonly IGenericAttributeService _genericAttributeService;

    #endregion

    #region Ctor

    public PublicOrderDetailsViewComponent(IHtmlFormatter htmlFormatter,
        IGenericAttributeService genericAttributeService)
    {
        _htmlFormatter = htmlFormatter;
        _genericAttributeService = genericAttributeService;
    }

    #endregion

    #region Methods

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var orderModel = additionalData as OrderDetailsModel;
        var giftMessage = await _genericAttributeService.GetAttributeAsync<Order, string>(orderModel.Id, CheckoutGiftMessageDefaults.GiftMessageAttributeName);

        if (string.IsNullOrEmpty(giftMessage))
            return Content("");

        var model = new OrderDetailsGiftMessageModel
        {
            GiftMessage = _htmlFormatter.FormatText(giftMessage, false, true, false, false, false, false)
        };

        return View("~/Plugins/Widgets.CheckoutGiftMessage/Views/PublicOrderDetails.cshtml", model);
    }

    #endregion
}
