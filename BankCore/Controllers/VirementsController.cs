﻿using System;
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
    [Route("api/Virements")]
    public class VirementsController : Controller
    {
        private readonly BankCoreContext _context;

        public VirementsController(BankCoreContext context)
        {
            _context = context;
        }

        // GET: api/Virements
        [Authorize]
        [HttpGet]
        public IEnumerable<Virement> GetVirements()
        {
            string currentUserId = User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;
            return HttpContext.User.IsInRole("Administrator") ? _context.Virements : _context.Virements.Where(v => v.sender_id.ToString() == currentUserId);
        }

        // GET: api/Virements/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVirement([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string currentUserId = User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;
            var virement = HttpContext.User.IsInRole("Administrator") ? await _context.Virements.SingleOrDefaultAsync(m => m._id == id) : await _context.Virements.SingleOrDefaultAsync(m => m._id == id && m.sender_id.ToString() == currentUserId);

            if (virement == null)
            {
                return NotFound();
            }

            return Ok(virement);
        }

        // PUT: api/Virements/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVirement([FromRoute] Guid id, [FromBody] Virement virement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != virement._id)
            {
                return BadRequest();
            }

            _context.Entry(virement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VirementExists(id))
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

        // POST: api/Virements
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostVirement([FromBody] Virement virement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string currentUserId = User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;
            var user = await _context.Users.SingleOrDefaultAsync(m => m._id.ToString() == currentUserId);

            if (user != null)
                return BadRequest();
            if (!user.canVir)
                return Unauthorized();

            _context.Virements.Add(virement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVirement", new { id = virement._id }, virement);
        }

        // DELETE: api/Virements/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVirement([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var virement = await _context.Virements.SingleOrDefaultAsync(m => m._id == id);
            if (virement == null)
            {
                return NotFound();
            }

            _context.Virements.Remove(virement);
            await _context.SaveChangesAsync();

            return Ok(virement);
        }

        private bool VirementExists(Guid id)
        {
            return _context.Virements.Any(e => e._id == id);
        }
    }
}