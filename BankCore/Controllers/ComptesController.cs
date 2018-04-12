using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankCore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BankCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Comptes")]
    public class ComptesController : Controller
    {
        private readonly BankCoreContext _context;

        public ComptesController(BankCoreContext context)
        {
            _context = context;
        }

        // GET: api/Comptes
        [Authorize]
        [HttpGet]
        public IEnumerable<Compte> GetComptes()
        {
            string currentUserId = User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;
            return HttpContext.User.IsInRole("Administrator") ?  _context.Comptes : _context.Comptes.Where(c => c.owner_id.ToString() == currentUserId);
        }

        // GET: api/Comptes/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompte([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string currentUserId = User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;

            var compte = HttpContext.User.IsInRole("Administrator") ? await _context.Comptes.SingleOrDefaultAsync(m => m._id == id) : await _context.Comptes.SingleOrDefaultAsync(m => m._id == id && m.owner_id.ToString() == currentUserId);

            if (compte == null)
            {
                return NotFound();
            }

            return Ok(compte);
        }

        // PUT: api/Comptes/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompte([FromRoute] Guid id, [FromBody] Compte compte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != compte._id)
            {
                return BadRequest();
            }

            _context.Entry(compte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompteExists(id))
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

        // POST: api/Comptes
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> PostCompte([FromBody] Compte compte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Comptes.Add(compte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompte", new { id = compte._id }, compte);
        }

        // DELETE: api/Comptes/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompte([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var compte = await _context.Comptes.SingleOrDefaultAsync(m => m._id == id);
            if (compte == null)
            {
                return NotFound();
            }

            _context.Comptes.Remove(compte);
            await _context.SaveChangesAsync();

            return Ok(compte);
        }

        private bool CompteExists(Guid id)
        {
            return _context.Comptes.Any(e => e._id == id);
        }
    }
}