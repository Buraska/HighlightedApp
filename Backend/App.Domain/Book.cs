using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Book : DomainEntityMetaId
{
    [MaxLength(64)]
    [MinLength(1)]
    public string Title { get; set; } = "UNDEFINED";
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Book), Name = nameof(SymbolsTotal))]
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

    public ICollection<Tag>? Tags { get; set; }
    
    public ICollection<Collect>? Collects { get; set; }
    
    public ICollection<Highlighted>? Highlighteds { get; set; }

    public AppUser? AppUser { get; set; } = default!;
    public Guid AppUserId { get; set; }

}

          