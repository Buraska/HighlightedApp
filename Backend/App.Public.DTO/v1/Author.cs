using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Author : DomainEntityId
{
    [MaxLength(64)]
    [MinLength(1)]
    public string Name { get; set; } = default!;
    
    [MaxLength(512)]
    public string Description { get; set; } = default!;

    public ICollection<Book>? Books { get; set; }
}