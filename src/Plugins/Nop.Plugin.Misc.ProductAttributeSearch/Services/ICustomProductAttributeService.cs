using Nop.Core;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Misc.ProductAttributeSearch.Services;

public interface ICustomProductAttributeService
{
    Task<IPagedList<ProductAttribute>> GetAllProductAttributesAsync(string searchName = null, int pageIndex = 0, 
        int pageSize = int.MaxValue);
}