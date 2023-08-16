using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser
{
    public ICollection<Book> Books { get; set; } = default!;
    
    public ICollection<Like> Likes { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(128)]
    public string LastName { get; set; } = default!;

    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}