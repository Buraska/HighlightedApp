using Base.Domain;

namespace App.Domain;

public class Preference : DomainEntityMetaId
{
    public int FontSize { get; set; } = 16;

    public FontFace? FontFace { get; set; }
    public Guid FontFaceId { get; set; }
}