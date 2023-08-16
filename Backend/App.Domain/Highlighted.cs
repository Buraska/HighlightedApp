using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Highlighted : DomainEntityMetaId
{
    [MaxLength(1000)]
    public string Content { get; set; } = default!;

    [MaxLength(2000)]
    public string? Comment { get; set; }
    
    public int OrderNo { get; set; }

    public int StartAt { get; set; }
    public int EndAt { get; set; }

    public Book? Book { get; set; }
    public Guid BookId { get; set; }

    public HighlightedType? HighlightedType { get; set; }
    public Guid HighlightedTypeId { get; set; }

    public ICollection<Like>? Likes { get; set; }

}