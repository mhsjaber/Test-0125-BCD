using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.DiscountRules.FromNthOrder.Models;

public class RequirementModel
{
    [NopResourceDisplayName("Plugins.DiscountRules.FromNthOrder.Fields.NthOrder")]
    public int NthOrder { get; set; }

    public int DiscountId { get; set; }

    public int RequirementId { get; set; }
}