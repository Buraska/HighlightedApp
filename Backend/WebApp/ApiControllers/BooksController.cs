#nullable disable
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.BLL.DTO;
using App.Contracts.BLL;
using App.Public.DTO;
using App.Public.DTO.v1.Identity;
using Base.DAL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DAL.App;
using Microsoft.AspNetCore.Identity;
using WebApp.ApiControllers.Identity;
using WebApp.DTO.Identity;
using AppUser = App.Domain.Identity.AppUser;


namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class BooksController : ControllerBase
    {
        private readonly BaseMapper<App.BLL.DTO.Book, App.Public.DTO.v1.Book> _mapper;
        private readonly IAppBLL _bll;
        private readonly ILogger<AccountController> _logger;


        public BooksController(IAppBLL bll, ILogger<AccountController> logger, IMapper mapper)
        {
            _bll = bll;
            _mapper = new BaseMapper<App.BLL.DTO.Book, App.Public.DTO.v1.Book>(mapper);
            _logger = logger;
            


        }

        // GET: api/Books
        /// <summary>   
        /// Gets all user books.   
        /// </summary>  
        /// <returns>All user books.</returns>    
        /// <response code="200">The books was successfully retrieved.</response>
        [Produces( "application/json" )]    
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Book> ), 200 )] 
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.Book>> GetBooks()
        {
            var books = (await _bll.Books.GetAllAsync());
            foreach (var b in books)
            {
                b.Content = "";
            }
            return books.Select(x => _mapper.Map(SetNavigationProperties(x)));
        }

        // GET: api/Books/5
        /// <summary>   
        /// Gets book by id.   
        /// </summary>  
        /// <returns>Book by id.</returns>    
        /// <response code="200">The books was successfully retrieved.</response>
        /// <response code="404">The books was not found for that user.</response>
        [Produces( "application/json" )]    
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Book> ), 200 )] 
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Book> ), 404 )] 
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Book>> GetBook(Guid id)
        {
            
            var book = (await _bll.Books.FirstOrDefaultAsync(id));

            
            if (book == null)
            {
                return NotFound();
            }

            SetNavigationProperties(book);

            return _mapper.Map(book);
        }

        private Book SetNavigationProperties(Book book)
        {
            var tags = _bll.BookTag.GetAll().Where(x => x.BookId == book.Id);
            book.Tags = new List<Tag>();
            foreach (var tag in tags)
            {

                book.Tags.Add(_bll.Tags.FirstOrDefault(tag.TagId));
            }

            var collects = _bll.BookCollect.GetAll().Where(x => x.BookId == book.Id);
            
            book.Collects = new List<Collect>();
            foreach (var item in collects)
            {
                book.Collects.Add(_bll.Collects.FirstOrDefault(item.CollectId));
            }
            
            book.Highlighteds = _bll.Highlighteds.GetAll().Where(x => x.BookId == book.Id).ToList();
            
            book.Language = _bll.Languages.FirstOrDefault(book.LanguageId);
            
            book.Preference = _bll.Preferences.FirstOrDefault(book.PreferenceId);
            book.Preference.FontFace = _bll.FontFaces.FirstOrDefault(book.Preference.FontFaceId);

            book.Author = _bll.Authors.FirstOrDefault(book.AuthorId);

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>   
        /// Update book.   
        /// </summary>  
        /// <returns>Task result.</returns>    
        /// <response code="200">The book was updated.</response>
        /// <response code="404">Something went wrong</response>
        [Produces( "application/json" )]    
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Book> ), 200 )] 
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Book> ), 404 )]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(Guid id, App.Public.DTO.v1.Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            var bllBook = _mapper.Map(book);

            PrepareForPut(bllBook);

            _bll.Books.Update(bllBook);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private Book PrepareForPut(Book book)
        {
            var allbt = _bll.BookTag.GetAll().Where(x => x.BookId == book.Id);
            foreach (var tag in allbt)
            {
                _bll.BookTag.Remove(tag);
            }
            foreach (var tag in book.Tags)
            {  
                _bll.BookTag.Add(new BookTag() {BookId = book.Id, TagId = tag.Id});
            }

            book.Tags = null;
                foreach (var item in book.Highlighteds)
            {
                if (!_bll.Highlighteds.GetAll().Any(x => x.BookId == item.BookId && x.StartAt == item.StartAt && item.EndAt == x.EndAt))
                {
                    item.Id = Guid.NewGuid();
                    item.BookId = item.BookId;
                    item.Book = null;
                    item.Likes = null;
                    item.HighlightedTypeId = _bll.HighlightedTypes.GetAll().FirstOrDefault(x => x.Name == item.HighlightedType.Name).Id;
                    item.HighlightedType = null;
                
                    _bll.Highlighteds.Add(item);
                }
                book.Highlighteds = null;
            }

            return book;

        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>   
        /// Add book .   
        /// </summary>  
        /// <returns>Task result</returns>    
        /// <response code="200">The books was successfully added.</response>
        /// <response code="404">Something went wrong.</response>
        [Produces( "application/json" )]    
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Book> ), 200 )] 
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Book> ), 404 )]
        [HttpPost]
        
        public async Task<ActionResult<App.Public.DTO.v1.Book>> PostBook(App.Public.DTO.v1.Book book)
        {

            var bllBook = _mapper.Map(book);
            
            bllBook.Id = Guid.NewGuid();
            
            bllBook.AppUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            bllBook.Preference = new Preference()
            {
                Id = Guid.NewGuid(),
                FontFaceId = _bll.FontFaces.GetAll().FirstOrDefault(x => x.Name == "arial").Id
            };

            PrepareForPost(bllBook);

            
            _bll.Books.Add(bllBook);
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetBook", new
            {
                id = book.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, book);
        }

        private Book PrepareForPost(Book book)
        { 
            book.AuthorId = book.Author.Id;
            book.Author = null;
            
            book.LanguageId = book.Language.Id;
            book.Language = null;

            foreach (var tag in book.Tags)
            {  
                _bll.BookTag.Add(new BookTag() {BookId = book.Id, TagId = tag.Id});
            }
            book.Tags = null;

            return new Book();
        }
        
        
        // DELETE: api/Books/5
        /// <summary>   
        /// Remove book by id.   
        /// </summary>  
        /// <returns>Task result.</returns>    
        /// <response code="200">The book was successfully deleted.</response>
        /// <response code="404">The book was not found for that user.</response>
        [Produces( "application/json" )]    
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Book> ), 200 )] 
        [ProducesResponseType( typeof( IEnumerable<App.Public.DTO.v1.Book> ), 404 )]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await _bll.Books.FirstOrDefaultAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _bll.Books.Remove(book);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private void PrepareForDelete(Book book)
        {
        }

        private bool BookExists(Guid id)
        {
            return _bll.Books.Exists(id);
        }
    }
}
