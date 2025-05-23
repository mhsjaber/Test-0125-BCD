using Microsoft.AspNetCore.Mvc;

namespace Nop.Plugin.Misc.OrderApi.Controllers;

[Area(OrderApiDefaults.PUBLIC_API)]
[Route(OrderApiDefaults.PUBLIC_API + "/[controller]/[action]", Order = int.MaxValue)]
[ApiExplorerSettings(GroupName = "ORDERS")]
public class ApiControllerBase : Controller
{ 

}
