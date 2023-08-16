using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Like : DomainEntityId
{
    public Highlighted? Highlighted { get; set; }
    public Guid HighlightedId { get; set; }

    public AppUser? AppUser { get; set; }
    public Guid AppUserId { get; set; }
}