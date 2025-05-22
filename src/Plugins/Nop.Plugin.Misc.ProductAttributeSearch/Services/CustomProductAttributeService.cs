using Nop.Core.Domain.Catalog;
using Nop.Core;
using Nop.Data;

namespace Nop.Plugin.Misc.ProductAttributeSearch.Services;

public class CustomProductAttributeService : ICustomProductAttributeService
{
    #region Fields

    protected readonly IRepository<ProductAttribute> _productAttributeRepository;

    #endregion

    #region Ctor

    public CustomProductAttributeService(IRepository<ProductAttribute> productAttributeRepository)
    {
        _productAttributeRepository = productAttributeRepository;
    }

    #endregion

    #region Methods

    public virtual async Task<IPagedList<ProductAttribute>> GetAllProductAttributesAsync(string searchName = null, int pageIndex = 0,
        int pageSize = int.MaxValue)
    {
        var productAttributes = await _productAttributeRepository.GetAllPagedAsync(query =>
        {
            if (!string.IsNullOrEmpty(searchName))
                query = query.Where(pa => pa.Name.Contains(searchName));

            return from pa in query
                   orderby pa.Name
                   select pa;
        }, pageIndex, pageSize);

        return productAttributes;
    }

    #endregion
}
