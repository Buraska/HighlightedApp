using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Base.Domain.Identity;

namespace App.DTO.Identity;

public class AppUser: BaseUser
{
    [MinLength(1)]
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    public ICollection<Book>? Meetings { get; set; }
    
    public string FirstLastName => FirstName + " " + LastName;
    public string LastFirstName => LastName + " " + FirstName;

    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}