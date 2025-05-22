using FluentValidation;
using Nop.Plugin.DiscountRules.FromNthOrder.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.DiscountRules.FromNthOrder.Validators;

/// <summary>
/// Represents an <see cref="RequirementModel"/> validator.
/// </summary>
public class RequirementModelValidator : BaseNopValidator<RequirementModel>
{
    public RequirementModelValidator(ILocalizationService localizationService)
    {
        RuleFor(model => model.DiscountId)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Plugins.DiscountRules.FromNthOrder.Fields.DiscountId.Required"));
        RuleFor(model => model.NthOrder)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Plugins.DiscountRules.FromNthOrder.Fields.NthOrder.Required"));
    }
}