using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.Identity;

public class Login
{
    [StringLength(maximumLength:128, MinimumLength = 5)] 
    public string Email { get; set; } = default!;
    
    public string Password { get; set; } = default!;
}