using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankCore.Models;

namespace BankCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Mouvements")]
    public class MouvementsController : Controller
    {
        private readonly BankCoreContext _context;

        public MouvementsController(BankCoreContext context)
        {
            _context = context;
        }

        // GET: api/Mouvements
        [HttpGet]
        public IEnumerable<Mouvement> GetMouvements()
        {
            return _context.Mouvements;
        }

        // GET: api/Mouvements/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMouvement([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mouvement = await _context.Mouvements.SingleOrDefaultAsync(m => m._id == id);

            if (mouvement == null)
            {
                return NotFound();
            }

            return Ok(mouvement);
        }

        // PUT: api/Mouvements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMouvement([FromRoute] Guid id, [FromBody] Mouvement mouvement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mouvement._id)
            {
                return BadRequest();
            }

            _context.Entry(mouvement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MouvementExists(id))
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

        // POST: api/Mouvements
        [HttpPost]
        public async Task<IActionResult> PostMouvement([FromBody] Mouvement mouvement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Mouvements.Add(mouvement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMouvement", new { id = mouvement._id }, mouvement);
        }

        // DELETE: api/Mouvements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMouvement([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mouvement = await _context.Mouvements.SingleOrDefaultAsync(m => m._id == id);
            if (mouvement == null)
            {
                return NotFound();
            }

            _context.Mouvements.Remove(mouvement);
            await _context.SaveChangesAsync();

            return Ok(mouvement);
        }

        private bool MouvementExists(Guid id)
        {
            return _context.Mouvements.Any(e => e._id == id);
        }
    }
}