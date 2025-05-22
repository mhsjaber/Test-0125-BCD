using Nop.Plugin.Misc.ProductAttributeSearch.Models;
using Nop.Web.Areas.Admin.Models.Catalog;

namespace Nop.Plugin.Misc.ProductAttributeSearch.Factories;
public interface ICustomProductAttributeModelFactory
{
    Task<ProductAttributeListModel> PrepareProductAttributeListModelAsync(OverriddenProductAttributeSearchModel searchModel);
    Task<OverriddenProductAttributeSearchModel> PrepareProductAttributeSearchModelAsync(OverriddenProductAttributeSearchModel searchModel);
}