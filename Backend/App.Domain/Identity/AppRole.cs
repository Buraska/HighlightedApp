using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Identity;

public class AppRole : IdentityRole<Guid>
{
    [MinLength(1)]
    [MaxLength(128)]
    public string DisplayName { get; set; } = default!;

}