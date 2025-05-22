using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.ProductAttributeSearch.Factories;
using Nop.Plugin.Misc.ProductAttributeSearch.Models;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.ProductAttributeSearch.Controllers;

public class OverriddenProductAttributeController : ProductAttributeController
{
    private readonly ICustomProductAttributeModelFactory _customProductAttributeModelFactory;

    public OverriddenProductAttributeController(ICustomerActivityService customerActivityService,
        ILocalizationService localizationService,
        ILocalizedEntityService localizedEntityService,
        INotificationService notificationService,
        IPermissionService permissionService,
        IProductAttributeModelFactory productAttributeModelFactory,
        IProductAttributeService productAttributeService,
        ICustomProductAttributeModelFactory customProductAttributeModelFactory)
        : base(customerActivityService,
            localizationService,
            localizedEntityService,
            notificationService,
            permissionService,
            productAttributeModelFactory,
            productAttributeService)
    {
        _customProductAttributeModelFactory = customProductAttributeModelFactory;
    }

    [CheckPermission(StandardPermission.Catalog.PRODUCT_ATTRIBUTES_VIEW)]
    public override async Task<IActionResult> List()
    {
        var model = await _customProductAttributeModelFactory.PrepareProductAttributeSearchModelAsync(new OverriddenProductAttributeSearchModel());

        return View("~/Plugins/Misc.ProductAttributeSearch/Views/List.cshtml", model);
    }

    [HttpPost]
    [CheckPermission(StandardPermission.Catalog.PRODUCT_ATTRIBUTES_VIEW)]
    public virtual async Task<IActionResult> GetList(OverriddenProductAttributeSearchModel searchModel)
    {
        //prepare model
        var model = await _customProductAttributeModelFactory.PrepareProductAttributeListModelAsync(searchModel);

        return Json(model);
    }
}
