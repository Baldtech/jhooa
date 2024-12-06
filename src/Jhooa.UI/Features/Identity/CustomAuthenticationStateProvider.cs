using System.Security.Claims;
using Jhooa.UI.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;

namespace Jhooa.UI.Features.Identity;

public class CustomAuthenticationStateProvider(ApplicationDbContext dbContext) : ServerAuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authState = await base.GetAuthenticationStateAsync();
        var user = authState.User;

        var claimId = user.Claims.FirstOrDefault(c =>
            string.Equals(c.Type, ClaimTypes.NameIdentifier, StringComparison.Ordinal))?.Value;
        if (!string.IsNullOrWhiteSpace(claimId))
        {
            var userDb = await dbContext.Users.FirstAsync(u =>
                u.Id == new Guid(claimId));
            
            user.AddIdentity(new ClaimsIdentity(new List<Claim>() { new Claim(ClaimTypes.GivenName, userDb.FirstName) }));
            user.AddIdentity(new ClaimsIdentity(new List<Claim>() { new Claim(ClaimTypes.Surname, userDb.LastName) }));
        }

        // return the modified principal
        return await Task.FromResult(new AuthenticationState(user));
    }
}