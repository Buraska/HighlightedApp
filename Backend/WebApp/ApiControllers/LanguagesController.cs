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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")] 

    public class LanguagesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper =
            new MapperConfiguration(cfg => cfg.CreateMap<Language, App.Public.DTO.v1.Language>().ReverseMap()).CreateMapper();

        public LanguagesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Languages
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<App.Public.DTO.v1.Language>> GetLanguages()
        {
            return (await _bll.Languages.GetAllAsync()).Select(x => _mapper.Map<App.Public.DTO.v1.Language>(x));
        }

        // GET: api/Languages/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<App.Public.DTO.v1.Language>> GetLanguage(Guid id)
        {
            var language = await _bll.Languages.FirstOrDefaultAsync(id);

            if (language == null)
            {
                return NotFound();
            }

            return _mapper.Map<App.Public.DTO.v1.Language>(language);
        }

        // PUT: api/Languages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguage(Guid id, App.Public.DTO.v1.Language language)
        {
            if (id != language.Id)
            {
                return BadRequest();
            }

            _bll.Languages.Update(_mapper.Map<Language>(language));

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageExists(id))
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

        // POST: api/Languages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Language>> PostLanguage(App.Public.DTO.v1.Language language)
        {
            _bll.Languages.Add(_mapper.Map<Language>(language));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetLanguage", new { id = language.Id }, language);
        }

        // DELETE: api/Languages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(Guid id)
        {
            var language = await _bll.Languages.FirstOrDefaultAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            _bll.Languages.Remove(language);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        [AllowAnonymous]
        private bool LanguageExists(Guid id)
        {
            return _bll.Languages.Exists( id);
        }
    }
}
