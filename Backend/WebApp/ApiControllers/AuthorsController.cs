#nullable disable
using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DAL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 

    public class AuthorsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper =
            new MapperConfiguration(cfg => cfg.CreateMap<Author, App.Public.DTO.v1.Author>().ReverseMap()).CreateMapper();

        public AuthorsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.Author>> GetAuthors()
        {
            return (await _bll.Authors.GetAllAsync()).Select(x => _mapper.Map<App.Public.DTO.v1.Author>(x));
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<App.Public.DTO.v1.Author>> GetAuthor(Guid id)
        {
            var author = await _bll.Authors.FirstOrDefaultAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return _mapper.Map<App.Public.DTO.v1.Author>(author);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutAuthor(Guid id, App.Public.DTO.v1.Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _bll.Authors.Update(_mapper.Map<Author>(author));

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Author>> PostAuthor(App.Public.DTO.v1.Author author)
        {
            _bll.Authors.Add(_mapper.Map<Author>(author));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var author = await _bll.Authors.FirstOrDefaultAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _bll.Authors.Remove(author);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(Guid id)
        {
            return _bll.Authors.Exists(id);
        }
    }
}
