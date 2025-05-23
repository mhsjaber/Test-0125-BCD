using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.OrderApi.Infrastructure;

public class JwtConfig : IConfig
{
    public string SecretKey { get; set; } = "a8J#fL9@pQ2zMv6X";

    public string Issuer { get; set; } = "BCD";

    public string Audience { get; set; } = "BCDUsers";

    public int ExpirationMinutes { get; set; } = 60 * 24 * 30;
}