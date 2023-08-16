using App.Contracts;
using App.Contracts.Identity;
using App.DAL.EF.Mapper;
using App.DAL.EF.Repositories;
using App.DTO;
using App.DTO.Identity;
using Base.DAL;
using Base.DAL.EF;
using DAL.App;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork
{
    private readonly AutoMapper.IMapper _mapper;
    public AppUOW(AppDbContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private IBookRepository? _books;
    
    public virtual IBookRepository Books =>
        _books ??= new BookRepository(UOWDbContext, new BookMapper(_mapper));
    
    

    private IAuthorRepository? _authors;

    public virtual IAuthorRepository Authors =>
        _authors ??= new AuthorRepository(UOWDbContext, new BaseMapper<Author, Domain.Author>(_mapper));
    
    
    private IPreferenceRepository? _preferences;

    public virtual IPreferenceRepository Preferences =>
        _preferences ??= new PreferenceRepository(UOWDbContext, new BaseMapper<Preference, Domain.Preference>(_mapper));
    
    private IFontFaceRepository? _fontFace;

    public virtual IFontFaceRepository FontFaces =>
        _fontFace ??= new FontFaceRepository(UOWDbContext, new BaseMapper<FontFace, Domain.FontFace>(_mapper));

    private ILanguageRepository? _language;

    public virtual ILanguageRepository Languages =>
        _language ??= new LanguageRepository(UOWDbContext, new BaseMapper<Language, Domain.Language>(_mapper));
    
    
    private IAppUserRepository? _appUsers;
    public virtual IAppUserRepository AppUsers =>
        _appUsers ??= new AppUserRepository(UOWDbContext, new BaseMapper<AppUser, Domain.Identity.AppUser>(_mapper));

    
    private IRefreshTokenRepository? _refreshTokens;
    public virtual IRefreshTokenRepository RefreshTokens =>
        _refreshTokens ??= new RefreshTokenRepository(UOWDbContext, new BaseMapper<RefreshToken, Domain.Identity.RefreshToken>(_mapper));

    
    private ICollectRepository? _collects;

    public virtual ICollectRepository Collects =>
        _collects ??= new CollectRepository(UOWDbContext, new BaseMapper<Collect, Domain.Collect>(_mapper));

        
    private IHighlightedRepository? _highlighteds;

    public virtual IHighlightedRepository Highlighteds =>
        _highlighteds ??= new HighlightedRepository(UOWDbContext, new BaseMapper<Highlighted, Domain.Highlighted>(_mapper));

    private IHighlightedTypeRepository? _highlightedTypes;

    public virtual IHighlightedTypeRepository HighlightedTypes =>
        _highlightedTypes ??= new HighlightedTypeRepository(UOWDbContext, new BaseMapper<HighlightedType, Domain.HighlightedType>(_mapper));
   
    private ILikeRepository? _likes;

    public virtual ILikeRepository Likes =>
        _likes ??= new LikeRepository(UOWDbContext, new BaseMapper<Like, Domain.Like>(_mapper));

    private ITagRepository? _tags;

    public virtual ITagRepository Tags =>
        _tags ??= new TagRepository(UOWDbContext, new BaseMapper<Tag, Domain.Tag>(_mapper));


    private IBookTagRepository? _bookTag;

    public virtual IBookTagRepository BookTag =>
        _bookTag ??= new BookTagRepository(UOWDbContext, new BaseMapper<BookTag, Domain.BookTag>(_mapper));


    private IBookCollectRepository? _bookCollect;

    public virtual IBookCollectRepository BookCollect =>
        _bookCollect ??= new BookCollectRepository(UOWDbContext, new BaseMapper<BookCollect, Domain.BookCollect>(_mapper));


}