using Ecommerce.API.Data;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Categories.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Category category)
        {
            try
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            catch (DbUpdateException update)
            {
                if (update.InnerException.Message.Contains("duplicate"))
                    return BadRequest("Ya existe una categoría con el mismo nombre");

                return BadRequest(update.InnerException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(Category category)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            catch (DbUpdateException update)
            {
                if (update.InnerException.Message.Contains("duplicate"))
                    return BadRequest("Ya existe una categoría con el mismo nombre");

                return BadRequest(update.InnerException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var affectedRows = await _context.Categories.Where(c => c.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}