using App.Contracts.Identity;
using Base.Contracts.DAL;

namespace App.Contracts;

public interface IAppUnitOfWork : IUnitOfWork
{
    IBookRepository Books { get; }
    IAuthorRepository Authors { get; }
    
    IPreferenceRepository Preferences { get; }
    
    IAppUserRepository AppUsers { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    
    ICollectRepository Collects { get; }
    IFontFaceRepository FontFaces { get; }
    IHighlightedRepository Highlighteds { get; }
    IHighlightedTypeRepository HighlightedTypes { get; }
    ILanguageRepository Languages { get; }
    ILikeRepository Likes { get; }
    ITagRepository Tags { get; }
    IBookTagRepository BookTag { get; }
    IBookCollectRepository BookCollect { get; }

    
}