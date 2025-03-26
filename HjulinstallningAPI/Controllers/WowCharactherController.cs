using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HjulinstallningAPI.Data;
using HjulinstallningAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HjulinstallningAPI.Controllers
{
    [ApiController]
    [Route("api/wowcharacter")]
    public class WowCharacterController : ControllerBase
    {
        private readonly VehicleDbContext _context;

        public WowCharacterController(VehicleDbContext context)
        {
            _context = context;
        }

        // ✅ GET: Retrieve all characters with filtering and sorting
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WowCharacter>>> GetCharacters(string? race, string? classType, string? sortBy)
        {
            var query = _context.WowCharacters.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(race))
            {
                query = query.Where(c => c.Race == race);
            }

            if (!string.IsNullOrEmpty(classType))
            {
                query = query.Where(c => c.Class == classType);
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "name" => query.OrderBy(c => c.Name),
                    "level" => query.OrderBy(c => c.Level),
                    _ => query
                };
            }

            return await query.ToListAsync();
        }

        // ✅ GET: Retrieve a character by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<WowCharacter>> GetCharacter(int id)
        {
            var character = await _context.WowCharacters.FindAsync(id);

            if (character == null)
                return NotFound();

            return character;
        }

        // ✅ POST: Add a new character
        [HttpPost]
        public async Task<ActionResult<WowCharacter>> AddCharacter(WowCharacter character)
        {
            // ✅ Validate BEFORE adding to the database
            if (string.IsNullOrWhiteSpace(character.ArmorType))
                return BadRequest("ArmorType is required.");

            _context.WowCharacters.Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCharacter), new { id = character.Id }, character);
        }


        // ✅ PUT: Update a character
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(int id, WowCharacter updatedCharacter)
        {
            if (id != updatedCharacter.Id)
                return BadRequest();

            _context.Entry(updatedCharacter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.WowCharacters.Any(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // ✅ DELETE: Remove a character
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character = await _context.WowCharacters.FindAsync(id);
            if (character == null)
                return NotFound();

            _context.WowCharacters.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
