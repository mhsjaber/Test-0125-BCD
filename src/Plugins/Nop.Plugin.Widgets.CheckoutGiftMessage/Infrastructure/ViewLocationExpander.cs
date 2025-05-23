using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Web.Framework;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Infrastructure;

public class ViewLocationExpander : IViewLocationExpander
{
    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        if (context.AreaName == AreaNames.ADMIN && context.ControllerName == "Order")
        { 
            viewLocations = new[] {
                $"/Plugins/Widgets.CheckoutGiftMessage/Areas/Admin/Views/{{1}}/{{0}}.cshtml",
                $"/Plugins/Widgets.CheckoutGiftMessage/Areas/Admin/Views/Shared/{{0}}.cshtml" }
            .Concat(viewLocations);
        }

        return viewLocations;
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
    }
}
