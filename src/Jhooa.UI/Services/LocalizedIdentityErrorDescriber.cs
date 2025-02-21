using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Jhooa.UI.Services;

internal sealed class LocalizedIdentityErrorDescriber(IStringLocalizer<Resources.LocalizedIdentityErrorDescriber> localizer)
    : IdentityErrorDescriber
{

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = localizer["PasswordRequiresNonAlphanumeric"]
        };
    }
    
    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = localizer["PasswordRequiresDigit"]
        };
    }
    
    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = localizer["PasswordRequiresLower"]
        };
    }
    
    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = localizer["PasswordRequiresUpper"]
        };
    }

    // Override other methods as needed
}