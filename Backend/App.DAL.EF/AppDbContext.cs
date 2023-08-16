using App.Domain;
using App.Domain.Identity;
using Base.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    public DbSet<Book> Books { get; set; } = default!;
    public DbSet<Author> Authors { get; set; } = default!;
    public DbSet<BookCollect> BookCollects { get; set; } = default!;
    public DbSet<BookTag> BookTags { get; set; } = default!;
    public DbSet<Collect> Collects { get; set; } = default!;
    public DbSet<Highlighted> Highlighted { get; set; } = default!;
    public DbSet<HighlightedType> HighlightedTypes { get; set; } = default!;
    public DbSet<Like> Likes { get; set; } = default!;
    public DbSet<Preference> Preferences { get; set; } = default!;
    public DbSet<Tag> Tags { get; set; } = default!;
    public DbSet<FontFace> FontFaces { get; set; } = default!;
    public DbSet<Language> Languages { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
    
    public override int SaveChanges()
    {
        FixEntities(this);
        
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        FixEntities(this);
        
        return base.SaveChangesAsync(cancellationToken);
    }

    private void FixEntities(AppDbContext context)
    {
        var dateProperties = context.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(z => new
            {
                ParentName = z.DeclaringEntityType.Name,
                PropertyName = z.Name
            });

        var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(x => x.Entity);
        

        foreach (var entity in editedEntitiesInTheDbContextGraph)
        {
            var entityFields = dateProperties.Where(d => d.ParentName == entity.GetType().FullName);

            foreach (var property in entityFields)
            {
                var prop = entity.GetType().GetProperty(property.PropertyName);

                if (prop == null)
                    continue;

                var originalValue = prop.GetValue(entity) as DateTime?;
                if (originalValue == null)
                    continue;

                prop.SetValue(entity, DateTime.SpecifyKind(originalValue.Value, DateTimeKind.Utc));
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}