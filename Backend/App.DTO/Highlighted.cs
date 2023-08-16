using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;

namespace App.DTO;

public class Highlighted : DomainEntityId
{
    public string Content { get; set; } = default!;

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