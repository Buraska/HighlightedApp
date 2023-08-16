using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.BLL.Services;
using App.BLL.Services.Identity;
using App.Contracts;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using AutoMapper;
using Base.BLL;
using Base.DAL;

namespace App.BLL;

public class AppBLL : BaseBll<IAppUnitOfWork>, IAppBLL
{
    protected IAppUnitOfWork UnitOfWork;
    private readonly AutoMapper.IMapper _mapper;

    public AppBLL(IAppUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public override async Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }

    private IBookService? _books;

    public IBookService Books =>
        _books ??= new BookService(UnitOfWork.Books, new BaseMapper<Book, App.DTO.Book>(_mapper));

    
    private IAppUserService? _appUsers;

    public IAppUserService AppUsers =>
        _appUsers ??= new AppUserService(UnitOfWork.AppUsers, new BaseMapper<AppUser, App.DTO.Identity.AppUser>(_mapper));

    private IRefreshTokenService? _refreshTokens;

    public IRefreshTokenService RefreshTokens =>
        _refreshTokens ??= new RefreshTokenService(UnitOfWork.RefreshTokens, new BaseMapper<RefreshToken, App.DTO.Identity.RefreshToken>(_mapper));

    private IAuthorService? _author;

    public IAuthorService Authors =>
        _author ??= new AuthorService(UnitOfWork.Authors, new BaseMapper<Author, App.DTO.Author>(_mapper));

    private ICollectService? _collects;

    public ICollectService Collects =>
        _collects ??= new CollectService(UnitOfWork.Collects, new BaseMapper<Collect, App.DTO.Collect>(_mapper));

    private IFontFaceService? _fontfaces;

    public IFontFaceService FontFaces =>
        _fontfaces ??= new FontFaceService(UnitOfWork.FontFaces, new BaseMapper<FontFace, App.DTO.FontFace>(_mapper));

    private IHighlightedService? _highlighteds;
    public IHighlightedService Highlighteds =>
        _highlighteds ??= new HighlightedService(UnitOfWork.Highlighteds, new BaseMapper<Highlighted, App.DTO.Highlighted>(_mapper));

    private IHighlightedTypeService? _highlightedTypes;
    public IHighlightedTypeService HighlightedTypes =>
        _highlightedTypes ??= new HighlightedTypeService(UnitOfWork.HighlightedTypes, new BaseMapper<HighlightedType, App.DTO.HighlightedType>(_mapper));

    private ILanguageService? _languages;
    public ILanguageService Languages =>
        _languages ??= new LanguageService(UnitOfWork.Languages, new BaseMapper<Language, App.DTO.Language>(_mapper));

    private ILikeService? _likes;
    public ILikeService Likes =>
        _likes ??= new LikeService(UnitOfWork.Likes, new BaseMapper<Like, App.DTO.Like>(_mapper));

    private IPreferenceService? _preference;
    public IPreferenceService Preferences =>
        _preference ??= new PreferenceService(UnitOfWork.Preferences, new BaseMapper<Preference, App.DTO.Preference>(_mapper));
    
    private ITagService? _tags;
    public ITagService Tags =>
        _tags ??= new TagService(UnitOfWork.Tags, new BaseMapper<Tag, App.DTO.Tag>(_mapper));

    private IBookTagService? _bookTag;
    public IBookTagService BookTag =>
        _bookTag ??= new BookTagService(UnitOfWork.BookTag, new BaseMapper<BookTag, App.DTO.BookTag>(_mapper));

    private IBookCollectService? _bookCollect;
    public IBookCollectService BookCollect =>
        _bookCollect ??= new BookCollectService(UnitOfWork.BookCollect, new BaseMapper<BookCollect, App.DTO.BookCollect>(_mapper));

}