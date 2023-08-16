using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Language : DomainEntityMetaId
{
    [MaxLength(32)]
    public string Name { get; set; } = "UNDEFINED";
}