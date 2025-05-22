using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Plugins;

namespace Nop.Plugin.DiscountRules.FromNthOrder;

public class FromNthOrderDiscountRequirementRule : BasePlugin, IDiscountRequirementRule
{
    #region Fields

    protected readonly IActionContextAccessor _actionContextAccessor;
    protected readonly ICustomerService _customerService;
    protected readonly IDiscountService _discountService;
    protected readonly ILocalizationService _localizationService;
    protected readonly ISettingService _settingService;
    protected readonly IUrlHelperFactory _urlHelperFactory;
    protected readonly IWebHelper _webHelper;
    protected readonly IOrderService _orderService;

    #endregion

    #region Ctor

    public FromNthOrderDiscountRequirementRule(IActionContextAccessor actionContextAccessor,
        IDiscountService discountService,
        ICustomerService customerService,
        ILocalizationService localizationService,
        ISettingService settingService,
        IUrlHelperFactory urlHelperFactory,
        IWebHelper webHelper,
        IOrderService orderService)
    {
        _actionContextAccessor = actionContextAccessor;
        _customerService = customerService;
        _discountService = discountService;
        _localizationService = localizationService;
        _settingService = settingService;
        _urlHelperFactory = urlHelperFactory;
        _webHelper = webHelper;
        _orderService = orderService;
    }

    #endregion

    #region Methods

    public async Task<DiscountRequirementValidationResult> CheckRequirementAsync(DiscountRequirementValidationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        //invalid by default
        var result = new DiscountRequirementValidationResult();

        if (request.Customer == null)
            return result;

        var nthOrder = await _settingService.GetSettingByKeyAsync<int>(string.Format(DiscountRequirementDefaults.SettingsKey, request.DiscountRequirementId));
        if (nthOrder == 0)
            return result;

        var customerOrders = await _orderService.SearchOrdersAsync(customerId: request.Customer.Id, getOnlyTotalCount: true);
        result.IsValid = customerOrders.TotalCount >= (nthOrder - 1);

        return result;
    }

    public string GetConfigurationUrl(int discountId, int? discountRequirementId)
    {
        var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

        return urlHelper.Action("Configure", "DiscountRulesFromNthOrder",
            new { discountId, discountRequirementId }, _webHelper.GetCurrentRequestProtocol());
    }

    public override async Task InstallAsync()
    {
        //locales
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.DiscountRules.FromNthOrder.Fields.NthOrder"] = "Nth order",
            ["Plugins.DiscountRules.FromNthOrder.Fields.NthOrder.Hint"] = "Enter the order number (N) from which the discount should start applying.",
            ["Plugins.DiscountRules.FromNthOrder.Fields.NthOrder.Required"] = "The value of 'N' must be 1 or greater.",
            ["Plugins.DiscountRules.FromNthOrder.Fields.DiscountId.Required"] = "Discount is required"
        });

        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        //discount requirements
        var discountRequirements = (await _discountService.GetAllDiscountRequirementsAsync())
            .Where(discountRequirement => discountRequirement.DiscountRequirementRuleSystemName == DiscountRequirementDefaults.SystemName);
        foreach (var discountRequirement in discountRequirements)
            await _discountService.DeleteDiscountRequirementAsync(discountRequirement, false);

        //locales
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.DiscountRules.FromNthOrder");

        await base.UninstallAsync();
    }

    #endregion
}