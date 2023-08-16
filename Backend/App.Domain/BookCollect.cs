using Base.Domain;

namespace App.Domain;

public class BookCollect : DomainEntityMetaId
{
    public Book? Book { get; set; }
    public Guid BookId { get; set; }
    
    public Collect? Collect { get; set; }
    public Guid CollectId { get; set; }
}