using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Like : DomainEntityId
{
    public Highlighted? Highlighted { get; set; }
    public Guid HighlightedId { get; set; }

    public AppUser? AppUser { get; set; }
    public Guid AppUserId { get; set; }
}