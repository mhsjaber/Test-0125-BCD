using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.CheckoutGiftMessage.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Factories;

namespace Nop.Plugin.Widgets.CheckoutGiftMessage.Infrastructure;

public class NopStartup : INopStartup
{
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrderModelFactory, OverriddenOrderModelFactory>();

        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new ViewLocationExpander());
        });

    }

    public void Configure(IApplicationBuilder application)
    {
    }

    public int Order => 3000;
}
