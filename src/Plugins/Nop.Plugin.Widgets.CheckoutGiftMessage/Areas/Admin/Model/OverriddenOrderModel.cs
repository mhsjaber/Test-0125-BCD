using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Areas.Admin.Model;

public record OverriddenOrderModel : OrderModel
{
    [NopResourceDisplayName("Admin.Orders.Fields.GiftMessage")]
    public string GiftMessage { get; set; }
}
