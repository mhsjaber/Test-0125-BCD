using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Services.Common;
using Nop.Services.Events;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Infrastructure;

public class EventConsumers : IConsumer<OrderPlacedEvent>
{
    private readonly IGenericAttributeService _genericAttributeService;
    private readonly IWorkContext _workContext;
    private readonly IStoreContext _storeContext;

    public EventConsumers(IGenericAttributeService genericAttributeService,
        IWorkContext workContext,
        IStoreContext storeContext)
    {
        _genericAttributeService = genericAttributeService;
        _workContext = workContext;
        _storeContext = storeContext;
    }

    public async Task HandleEventAsync(OrderPlacedEvent eventMessage)
    {
        var customer = await _workContext.GetCurrentCustomerAsync();
        var store = await _storeContext.GetCurrentStoreAsync();

        var attributes = await _genericAttributeService.GetAttributesForEntityAsync(customer.Id, nameof(Customer));
        if (attributes.FirstOrDefault(ga => ga.Key == CheckoutGiftMessageDefaults.GiftMessageAttributeName && ga.StoreId == store.Id) is GenericAttribute gm)
        {
            await _genericAttributeService.SaveAttributeAsync(eventMessage.Order, CheckoutGiftMessageDefaults.GiftMessageAttributeName, gm.Value);
            await _genericAttributeService.DeleteAttributeAsync(gm);
        }
    }
}