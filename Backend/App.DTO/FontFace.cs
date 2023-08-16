using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.DTO;

public class FontFace : DomainEntityId
{

    [MaxLength(32)] public string Name { get; set; } = default!;
}