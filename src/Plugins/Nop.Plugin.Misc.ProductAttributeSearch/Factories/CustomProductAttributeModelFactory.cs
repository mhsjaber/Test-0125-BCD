using Nop.Plugin.Misc.ProductAttributeSearch.Models;
using Nop.Plugin.Misc.ProductAttributeSearch.Services;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.ProductAttributeSearch.Factories;

public class CustomProductAttributeModelFactory : ICustomProductAttributeModelFactory
{
    private readonly ICustomProductAttributeService _productAttributeService;

    public CustomProductAttributeModelFactory(ICustomProductAttributeService productAttributeService)
    {
        _productAttributeService = productAttributeService;
    }

    public Task<OverriddenProductAttributeSearchModel> PrepareProductAttributeSearchModelAsync(OverriddenProductAttributeSearchModel searchModel)
    {
        ArgumentNullException.ThrowIfNull(searchModel);

        //prepare page parameters
        searchModel.SetGridPageSize();

        return Task.FromResult(searchModel);
    }

    public async Task<ProductAttributeListModel> PrepareProductAttributeListModelAsync(OverriddenProductAttributeSearchModel searchModel)
    {
        ArgumentNullException.ThrowIfNull(searchModel);

        //get product attributes
        var productAttributes = await _productAttributeService
            .GetAllProductAttributesAsync(searchName: searchModel.SearchName, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

        //prepare list model
        var model = new ProductAttributeListModel().PrepareToGrid(searchModel, productAttributes, () =>
        {
            //fill in model values from the entity
            return productAttributes.Select(attribute => attribute.ToModel<ProductAttributeModel>());

        });

        return model;
    }
}
