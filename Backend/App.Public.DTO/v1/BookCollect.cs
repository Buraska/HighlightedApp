using Base.Domain;

namespace App.Public.DTO.v1;

public class BookCollect : DomainEntityId
{

    public Book? Book { get; set; }
    public Guid BookId { get; set; }

    public Collect? Collect { get; set; }
    public Guid CollectId { get; set; }
}