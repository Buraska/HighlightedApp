using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Like : DomainEntityMetaId
{
    public Highlighted? Highlighted { get; set; }
    public Guid HighlightedId { get; set; }

    public AppUser? AppUser { get; set; }
    public Guid AppUserId { get; set; }
}