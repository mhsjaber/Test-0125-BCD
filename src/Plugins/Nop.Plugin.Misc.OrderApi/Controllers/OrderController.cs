using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.OrderApi.Models;
using Nop.Services.Catalog;
using Nop.Services.Helpers;
using Nop.Services.Orders;
using Nop.Services.Security;

namespace Nop.Plugin.Misc.OrderApi.Controllers;

[Authorize(AuthenticationSchemes = "JWT")]
public class OrderController : ApiControllerBase
{
    private readonly IWorkContext _workContext;
    private readonly IPermissionService _permissionService;
    private readonly IOrderService _orderService;
    private readonly IDateTimeHelper _dateTimeHelper;
    private readonly IPriceFormatter _priceFormatter;

    public OrderController(IWorkContext workContext,
        IPermissionService permissionService,
        IOrderService orderService,
        IDateTimeHelper dateTimeHelper,
        IPriceFormatter priceFormatter)
    {
        _workContext = workContext;
        _permissionService = permissionService;
        _orderService = orderService;
        _dateTimeHelper = dateTimeHelper;
        _priceFormatter = priceFormatter;
    }

    [HttpGet]
    public async Task<IActionResult> GetByEmail(string email, int page)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermission.Orders.ORDERS_VIEW))
            return BadRequest();

        var pageIndex = page - 1;
        pageIndex = pageIndex < 0 ? 0 : pageIndex;

        var language =  await _workContext.GetWorkingLanguageAsync();
        var orders = await _orderService.SearchOrdersAsync(billingEmail: email, pageIndex: pageIndex, pageSize: 10);
        
        var model = new List<OrderModel>();
        foreach (var order in orders)
        {
            model.Add(new OrderModel
            {
                TotalAmount = await _priceFormatter.FormatPriceAsync(order.OrderTotal, true, order.CustomerCurrencyCode, false, language.Id),
                Id = order.Id,
                OrderDate = await _dateTimeHelper.ConvertToUserTimeAsync(order.CreatedOnUtc)
            });
        }

        return Ok(model);
    }
}
