using App.Domain.Identity;
using Base.Domain;

namespace App.DTO;

public class Like : DomainEntityId
{
    public DTO.Highlighted? Highlighted { get; set; }
    public Guid HighlightedId { get; set; }

    public AppUser? AppUser { get; set; }
    public Guid AppUserId { get; set; }
}