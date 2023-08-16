using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Tag : DomainEntityId
{
    [MaxLength(32)]
    public string Name { get; set; } = default!;
}