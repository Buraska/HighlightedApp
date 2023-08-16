using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class HighlightedType : DomainEntityMetaId

{
    [MaxLength(32)]
    public string Name { get; set; } = default!;
}