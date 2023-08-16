using Base.Domain;

namespace App.DTO;

public class BookCollect : DomainEntityId
{

    public Book? Book { get; set; }
    public Guid BookId { get; set; }

    public Collect? Collect { get; set; }
    public Guid CollectId { get; set; }
}