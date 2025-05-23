using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.OrderApi.Models;

public record OrderModel
{
    public int Id { get; set; }

    public string TotalAmount { get; set; }

    public DateTime OrderDate { get; set; }
}