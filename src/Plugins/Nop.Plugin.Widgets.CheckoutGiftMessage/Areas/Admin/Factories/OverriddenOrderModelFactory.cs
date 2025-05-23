using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Plugin.Widgets.CheckoutGiftMessage.Areas.Admin.Infrastructure;
using Nop.Services.Affiliates;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Helpers;
using Nop.Services.Html;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Framework.UI;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Areas.Admin.Factories;

public class OverriddenOrderModelFactory : OrderModelFactory
{
    private readonly IGenericAttributeService _genericAttributeService;
    private readonly IHtmlFormatter _htmlFormatter;

    public OverriddenOrderModelFactory(
        AddressSettings addressSettings,
        CatalogSettings catalogSettings,
        CurrencySettings currencySettings,
        IActionContextAccessor actionContextAccessor,
        IAddressModelFactory addressModelFactory,
        IAddressService addressService,
        IAffiliateService affiliateService,
        IBaseAdminModelFactory baseAdminModelFactory,
        ICountryService countryService,
        ICurrencyService currencyService,
        ICustomerService customerService,
        IDateTimeHelper dateTimeHelper,
        IDiscountService discountService,
        IDownloadService downloadService,
        IEncryptionService encryptionService,
        IGiftCardService giftCardService,
        ILocalizationService localizationService,
        IMeasureService measureService,
        IOrderProcessingService orderProcessingService,
        IOrderReportService orderReportService,
        IOrderService orderService,
        IPaymentPluginManager paymentPluginManager,
        IPaymentService paymentService,
        IPictureService pictureService,
        IPriceCalculationService priceCalculationService,
        IPriceFormatter priceFormatter,
        IProductAttributeService productAttributeService,
        IProductService productService,
        IReturnRequestService returnRequestService,
        IRewardPointService rewardPointService,
        ISettingService settingService,
        IShipmentService shipmentService,
        IShippingService shippingService,
        IStateProvinceService stateProvinceService,
        IStoreService storeService,
        ITaxService taxService,
        IUrlHelperFactory urlHelperFactory,
        IVendorService vendorService,
        IWorkContext workContext,
        MeasureSettings measureSettings,
        NopHttpClient nopHttpClient,
        OrderSettings orderSettings,
        ShippingSettings shippingSettings,
        IUrlRecordService urlRecordService,
        TaxSettings taxSettings,
        IGenericAttributeService genericAttributeService,
        IHtmlFormatter htmlFormatter)
        : base(
            addressSettings,
            catalogSettings,
            currencySettings,
            actionContextAccessor,
            addressModelFactory,
            addressService,
            affiliateService,
            baseAdminModelFactory,
            countryService,
            currencyService,
            customerService,
            dateTimeHelper,
            discountService,
            downloadService,
            encryptionService,
            giftCardService,
            localizationService,
            measureService,
            orderProcessingService,
            orderReportService,
            orderService,
            paymentPluginManager,
            paymentService,
            pictureService,
            priceCalculationService,
            priceFormatter,
            productAttributeService,
            productService,
            returnRequestService,
            rewardPointService,
            settingService,
            shipmentService,
            shippingService,
            stateProvinceService,
            storeService,
            taxService,
            urlHelperFactory,
            vendorService,
            workContext,
            measureSettings,
            nopHttpClient,
            orderSettings,
            shippingSettings,
            urlRecordService,
            taxSettings)
    {
        _genericAttributeService = genericAttributeService;
        _htmlFormatter = htmlFormatter;
    }

    public override async Task<OrderModel> PrepareOrderModelAsync(OrderModel model, Order order, bool excludeProperties = false)
    {
        model = await base.PrepareOrderModelAsync(model, order, excludeProperties);

        var omoderl = model.ToOverriddenModel();
        
        var giftMessage = await _genericAttributeService.GetAttributeAsync<string>(order, CheckoutGiftMessageDefaults.GiftMessageAttributeName);
        if (!string.IsNullOrEmpty(giftMessage))
           omoderl.GiftMessage = _htmlFormatter.FormatText(giftMessage, false, true, false, false, false, false);

        return omoderl;
    }
}
