using App.Domain;
using Base.Domain;

namespace App.DTO;

public class BookTag : DomainEntityId
{
    public Domain.Book? Book { get; set; }
    public Guid BookId { get; set; }

    public Tag? Tag { get; set; }
    public Guid TagId { get; set; }
}