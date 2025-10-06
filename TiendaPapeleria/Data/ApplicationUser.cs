using Microsoft.AspNetCore.Identity;

namespace TiendaPapeleria.Data;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }
}
