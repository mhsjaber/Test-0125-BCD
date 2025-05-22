namespace Nop.Plugin.DiscountRules.FromNthOrder;

/// <summary>
/// Represents defaults for the discount requirement rule
/// </summary>
public static class DiscountRequirementDefaults
{
    /// <summary>
    /// The system name of the discount requirement rule
    /// </summary>
    public static string SystemName => "DiscountRequirement.FromNthOrder";

    /// <summary>
    /// The key of the settings to save restricted customer roles
    /// </summary>
    public static string SettingsKey => "DiscountRequirement.FromNthOrder-{0}";

    /// <summary>
    /// The HTML field prefix for discount requirements
    /// </summary>
    public static string HtmlFieldPrefix => "DiscountRulesFromNthOrder{0}";
}