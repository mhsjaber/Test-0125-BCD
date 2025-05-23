using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Models;

public record ShoppingCartGiftMessageModel : BaseNopEntityModel
{
    public string GiftMessage { get; set; }
}
