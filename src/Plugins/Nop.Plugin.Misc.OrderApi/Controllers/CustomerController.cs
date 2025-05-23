using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Core.Http.Extensions;
using Nop.Plugin.Misc.OrderApi.Models;
using Nop.Plugin.Misc.OrderApi.Services;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Web.Models.Customer;

namespace Nop.Plugin.Misc.OrderApi.Controllers;

public class CustomerController : ApiControllerBase
{
    private readonly CustomerSettings _customerSettings;
    private readonly ICustomerRegistrationService _customerRegistrationService;
    private readonly ICustomerService _customerService;
    private readonly ILocalizationService _localizationService;
    private readonly JwtTokenService _jwtTokenService;

    public CustomerController(CustomerSettings customerSettings,
        ICustomerRegistrationService customerRegistrationService,
        ICustomerService customerService,
        ILocalizationService localizationService,
        JwtTokenService jwtTokenService)
    {
        _customerSettings = customerSettings;
        _customerRegistrationService = customerRegistrationService;
        _customerService = customerService;
        _localizationService = localizationService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var customerUserName = model.Username;
        var customerEmail = model.Email;
        var userNameOrEmail = _customerSettings.UsernamesEnabled ? customerUserName : customerEmail;
        var responseModel = new LoginResponseModel();

        var loginResult = await _customerRegistrationService.ValidateCustomerAsync(userNameOrEmail, model.Password);
        switch (loginResult)
        {
            case CustomerLoginResults.Successful:
                {
                    var customer = _customerSettings.UsernamesEnabled
                        ? await _customerService.GetCustomerByUsernameAsync(customerUserName)
                        : await _customerService.GetCustomerByEmailAsync(customerEmail);

                    responseModel.Success = true;
                    responseModel.Token = _jwtTokenService.GenerateToken(customer);
                    
                    return Ok(responseModel);
                }
            case CustomerLoginResults.MultiFactorAuthenticationRequired:
                {
                    responseModel.Message = await _localizationService.GetResourceAsync("Account.Login.WrongCredentials.MultiFactorAuthenticationRequired");
                    break;
                }
            case CustomerLoginResults.CustomerNotExist:
                responseModel.Message = await _localizationService.GetResourceAsync("Account.Login.WrongCredentials.CustomerNotExist");
                break;
            case CustomerLoginResults.Deleted:
                responseModel.Message = await _localizationService.GetResourceAsync("Account.Login.WrongCredentials.Deleted");
                break;
            case CustomerLoginResults.NotActive:
                responseModel.Message = await _localizationService.GetResourceAsync("Account.Login.WrongCredentials.NotActive");
                break;
            case CustomerLoginResults.NotRegistered:
                responseModel.Message = await _localizationService.GetResourceAsync("Account.Login.WrongCredentials.NotRegistered");
                break;
            case CustomerLoginResults.LockedOut:
                responseModel.Message = await _localizationService.GetResourceAsync("Account.Login.WrongCredentials.LockedOut");
                break;
            case CustomerLoginResults.WrongPassword:
            default:
                responseModel.Message = await _localizationService.GetResourceAsync("Account.Login.WrongCredentials");
                break;
        }
        return BadRequest(responseModel);
    }
}
