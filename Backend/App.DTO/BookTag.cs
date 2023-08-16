using Base.Domain;

namespace App.DTO;

public class BookTag : DomainEntityMetaId
{
    public Book? Book { get; set; }
    public Guid BookId { get; set; }

    public Tag? Tag { get; set; }
    public Guid TagId { get; set; }
}