using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Book: DomainEntityId
{
    public string Title { get; set; } = "UNDEFINED";
    
    public int SymbolsTotal { get; set; }
    public int CurrentSymbol { get; set; }

    public string Content { get; set; } = default!;

    public DateTime LastAccessAt { get; set; } = DateTime.UtcNow;

    public bool IsFinished { get; set; } = false;
    
    public Preference? Preference { get; set; }
    public Guid PreferenceId { get; set; }
    
    public Author? Author { get; set; } = default!; 
    public Guid AuthorId { get; set; }
    
    public Language? Language { get; set; } = default!;
    public Guid LanguageId { get; set; }
    
    
    public ICollection<Collect>? Collects { get; set; }
    
    public ICollection<Tag>? Tags { get; set; }
    
    public ICollection<Highlighted>? Highlighteds { get; set; }

    public AppUser? AppUser { get; set; } = default!;
    public Guid AppUserId { get; set; }

    
    // create DTO
    // change repo param type. Remove ref from proj definition
}

