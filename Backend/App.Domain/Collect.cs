using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Collect : DomainEntityMetaId
{
    [MaxLength(64)]
    public string Title { get; set; } = "UNDEFINED";

    [MaxLength(512)] public string Description { get; set; } = "";

    public ICollection<Book>? Books { get; set; }
}