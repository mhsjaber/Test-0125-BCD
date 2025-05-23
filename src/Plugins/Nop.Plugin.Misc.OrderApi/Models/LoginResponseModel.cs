namespace Nop.Plugin.Misc.OrderApi.Models;

public class LoginResponseModel
{
    public string Token { get; set; }

    public bool Success { get; set; }

    public string Message { get; set; }
}
