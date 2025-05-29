using Ecommerce.API.Data;
using Ecommerce.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            try
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (DbUpdateException update)
            {
                if (update.InnerException.Message.Contains("duplicate"))
                    return BadRequest("Ya existe un usuario con el mismo email o nombre de usuario");

                return BadRequest(update.InnerException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(User user)
        {
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (DbUpdateException update)
            {
                if (update.InnerException.Message.Contains("duplicate"))
                    return BadRequest("Ya existe un usuario con el mismo email o nombre de usuario");

                return BadRequest(update.InnerException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.Users.FindAsync(id); // FindAsync funciona con string
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}