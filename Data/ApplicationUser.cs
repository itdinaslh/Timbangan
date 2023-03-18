using Microsoft.AspNetCore.Identity;

namespace Timbangan.Data;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
