using AutoMapper;
using Nop.Core;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Widgets.CheckoutGiftMessage.Areas.Admin.Model;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Areas.Admin.Infrastructure;

public class MapperConfiguration : Profile, IOrderedMapperProfile
{
    public MapperConfiguration()
    {
        CreateMap<OrderModel, OverriddenOrderModel>();
        CreateMap<OverriddenOrderModel, OrderModel>();
    }

    public int Order => 0;
}
