using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly DataContext _context;

        public ConfigController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Config
        [HttpGet]
        public IEnumerable<Config> GetConfigs()
        {
            return _context.Configs;
        }

        // GET: api/Config/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConfig([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var config = await _context.Configs.FindAsync(id);

            if (config == null)
            {
                return NotFound();
            }

            return Ok(config);
        }

        // PUT: api/Config/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfig([FromRoute] int id, [FromBody] Config config)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != config.Id)
            {
                return BadRequest();
            }

            _context.Entry(config).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigExists(id))
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

        // POST: api/Config
        [HttpPost]
        public async Task<IActionResult> PostConfig([FromBody] Config config)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Configs.Add(config);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfig", new { id = config.Id }, config);
        }

        // DELETE: api/Config/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfig([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var config = await _context.Configs.FindAsync(id);
            if (config == null)
            {
                return NotFound();
            }

            _context.Configs.Remove(config);
            await _context.SaveChangesAsync();

            return Ok(config);
        }

        private bool ConfigExists(int id)
        {
            return _context.Configs.Any(e => e.Id == id);
        }
    }
}