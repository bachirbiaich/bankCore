﻿using System;
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
    [Route("api/Comptes")]
    public class ComptesController : Controller
    {
        private readonly BankCoreContext _context;

        public ComptesController(BankCoreContext context)
        {
            _context = context;
        }

        // GET: api/Comptes
        [HttpGet]
        public IEnumerable<Compte> GetComptes()
        {
            return _context.Comptes;
        }

        // GET: api/Comptes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompte([FromRoute] Guid id)
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

            return Ok(compte);
        }

        // PUT: api/Comptes/5
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