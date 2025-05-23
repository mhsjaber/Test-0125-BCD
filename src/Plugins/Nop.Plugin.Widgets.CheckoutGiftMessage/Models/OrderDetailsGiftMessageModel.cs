using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Models;

public record OrderDetailsGiftMessageModel : BaseNopEntityModel
{
    public string GiftMessage { get; set; }
}
