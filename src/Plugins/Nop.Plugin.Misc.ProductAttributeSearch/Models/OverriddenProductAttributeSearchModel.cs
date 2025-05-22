using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.ProductAttributeSearch.Models;

public record OverriddenProductAttributeSearchModel : ProductAttributeSearchModel
{
    [NopResourceDisplayName("Admin.Catalog.Attributes.ProductAttributes.List.SearchName")]
    public string SearchName { get; set; }
}
