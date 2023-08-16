using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.BLL.DTO;

public class HighlightedType : DomainEntityId

{
    [MaxLength(32)]
    public string Name { get; set; } = default!;
}