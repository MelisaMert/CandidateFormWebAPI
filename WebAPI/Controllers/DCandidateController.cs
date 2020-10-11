using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DCandidateController : ControllerBase
    {
        private readonly DonationDBContext _context;
        public DCandidateController(DonationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DCandidate>>> GetCandidates()
        {
            return await _context.DCandidates.ToListAsync();
        }
        //Get api/DCandidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DCandidate>> GetCandidate(int id)
        {
            var dCandidate = await _context.DCandidates.FindAsync(id);
            if(dCandidate == null)
            {
                return NotFound();
            }
            return dCandidate;
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDCandidate(int id, DCandidate dCandidate)
        {
            if(id != dCandidate.id)
            {
                return BadRequest();
            }
            _context.Entry(dCandidate).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DCandidateExists(id))
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
        
        //POST api/DCandidate
        [HttpPost]
        public async Task<ActionResult<DCandidate>> PostDCandidate(DCandidate dCandidate)
        {
            _context.DCandidates.Add(dCandidate);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDCandidate", new { id = dCandidate.id }, dCandidate);
        }
        // DELETE api/DCandidate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DCandidate>> DeleteDCandidate(int id)
        {
            var dCandidate = await _context.DCandidates.FindAsync(id);
            if(dCandidate == null)
            {
                return NotFound();
            }
            _context.DCandidates.Remove(dCandidate);
            await _context.SaveChangesAsync();
            return dCandidate;
        }
        private bool DCandidateExists(int id)
        {
            return _context.DCandidates.Any(e => e.id == id);
        }
    }
}
