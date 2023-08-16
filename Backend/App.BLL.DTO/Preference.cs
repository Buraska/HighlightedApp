using Base.Domain;

namespace App.BLL.DTO;

public class Preference : DomainEntityId
{
    public int FontSize { get; set; } = 16;

    public FontFace? FontFace { get; set; }
    public Guid FontFaceId { get; set; }
}