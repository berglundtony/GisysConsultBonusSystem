using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GisysConsultBonusSystem.Models;
using Microsoft.AspNetCore.Cors;

namespace GisysConsultBonusSystem.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultsController : ControllerBase
    {
        private readonly ConsultContext _context;

        public ConsultsController(ConsultContext context)
        {
            _context = context;
        }

        // GET: api/Consults
        [HttpGet]
        [Route("~/api/Consults/")]
        public async Task<ActionResult<IEnumerable<Consult>>> GetConsults()
        {
            return await _context.Consults.ToListAsync();
        }

        // GET: api/Consults/5
        [HttpGet("{id}")]
        [EnableCors("DevelopmentCorsPolicy")]
        public async Task<ActionResult<Consult>> GetConsult(int id)
        {
            var consult = await _context.Consults.FindAsync(id);

            if (consult == null)
            {
                return NotFound();
            }

            return consult;
        }


        // POST: api/Consults
        [HttpPost]
        public async Task<ActionResult<Consult>> PostConsults([FromBody]Consult consult)
        {
            _context.Consults.Add(consult);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetConsult", new { id = consult.ConsultID }, consult);
        }

        // DELETE: api/Consults/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Consult>> DeleteConsults(int id)
        {
            var consult = await _context.Consults.FindAsync(id);
            if (consult == null)
            {
                return NotFound();
            }

            _context.Consults.Remove(consult);
            await _context.SaveChangesAsync();

            return consult;
        }

        // PUT: api/Consults/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsults(int id, Consult consult)
        {
            if (id != consult.ConsultID)
            {
                return BadRequest();
            }

            _context.Entry(consult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultExists(id))
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


        private bool ConsultExists(int id)
        {
            return _context.Consults.Any(e => e.ConsultID == id);
        }
    }
}
