using Base.Domain;

namespace App.DTO;

public class Preference : DomainEntityId
{
    public int FontSize { get; set; } = 16;

    public DTO.FontFace? FontFace { get; set; }
    public Guid FontFaceId { get; set; }
}