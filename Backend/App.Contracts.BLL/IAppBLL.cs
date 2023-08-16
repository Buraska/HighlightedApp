using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    IBookService Books { get; }
    IAppUserService AppUsers { get; }
    
    IRefreshTokenService RefreshTokens { get; }
    IAuthorService Authors { get; }
    ICollectService Collects { get; }
    IFontFaceService FontFaces { get; }
    IHighlightedService Highlighteds { get; }
    IHighlightedTypeService HighlightedTypes { get; }
    ILanguageService Languages { get; }
    ILikeService Likes { get; }
    IPreferenceService Preferences { get; }
    ITagService Tags { get; }
    
    IBookTagService BookTag { get; }
    IBookCollectService BookCollect { get; }
    
    
}