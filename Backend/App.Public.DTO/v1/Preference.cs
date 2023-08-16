using Base.Domain;

namespace App.Public.DTO.v1;

public class Preference : DomainEntityId
{
    public int FontSize { get; set; } = 16;

    public FontFace? FontFace { get; set; }
    public Guid FontFaceId { get; set; }
}