using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.ProductAttributeSearch.Controllers;
using Nop.Plugin.Misc.ProductAttributeSearch.Factories;
using Nop.Plugin.Misc.ProductAttributeSearch.Services;
using Nop.Web.Areas.Admin.Controllers;

namespace Nop.Plugin.Misc.ProductAttributeSearch.Infrastructure;

public class NopStartup : INopStartup
{
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICustomProductAttributeService, CustomProductAttributeService>();
        services.AddScoped<ICustomProductAttributeModelFactory, CustomProductAttributeModelFactory>();

        services.AddScoped<ProductAttributeController, OverriddenProductAttributeController>();
    }

    public void Configure(IApplicationBuilder application)
    {
    }

    public int Order => 3000;
}