#nullable disable
using App.BLL.DTO;
using App.Contracts.BLL;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ("admin"))] 

    public class TagsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper =
            new MapperConfiguration(cfg => cfg.CreateMap<Tag, App.Public.DTO.v1.Tag>().ReverseMap()).CreateMapper();

        public TagsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Tags
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<App.Public.DTO.v1.Tag>> GetTags()
        {
            return (await _bll.Tags.GetAllAsync()).Select(x => _mapper.Map<App.Public.DTO.v1.Tag>(x));
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<App.Public.DTO.v1.Tag>> GetTag(Guid id)
        {
            var tag = await _bll.Tags.FirstOrDefaultAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return _mapper.Map<App.Public.DTO.v1.Tag>(tag);
        }

        // PUT: api/Tags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(Guid id, App.Public.DTO.v1.Tag tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }

            _bll.Tags.Update(_mapper.Map<Tag>(tag));

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Tag>> PostTag(App.Public.DTO.v1.Tag tag)
        {
            _bll.Tags.Add(_mapper.Map<Tag>(tag));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTag", new { id = tag.Id }, tag);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var tag = await _bll.Tags.FirstOrDefaultAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _bll.Tags.Remove(tag);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous]
        private bool TagExists(Guid id)
        {
            return _bll.Tags.Exists(id);
        }
    }
}
