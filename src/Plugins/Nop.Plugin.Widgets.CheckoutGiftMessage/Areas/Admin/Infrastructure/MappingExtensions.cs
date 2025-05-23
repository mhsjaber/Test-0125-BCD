using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Widgets.CheckoutGiftMessage.Areas.Admin.Model;
using Nop.Web.Areas.Admin.Models.Orders;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Areas.Admin.Infrastructure;

public static class MappingExtensions
{
    private static TDestination Map<TDestination>(this object source)
    {
        //use AutoMapper for mapping objects
        return AutoMapperConfiguration.Mapper.Map<TDestination>(source);
    }

    public static OverriddenOrderModel ToOverriddenModel(this OrderModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return model.Map<OverriddenOrderModel>();
    }
}