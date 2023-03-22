﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Timbangan.Data;

namespace Timbangan.Helpers;

public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options) :
        base(userManager, roleManager, options)
    { }

#nullable disable
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = (await base.GenerateClaimsAsync(user));
        identity.AddClaim(new Claim("UserFullName", user.FullName));

        return identity;
    }
}
