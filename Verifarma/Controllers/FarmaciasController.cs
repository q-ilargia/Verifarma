using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Verifarma.Data;
using Verifarma.Models;
using Verifarma.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Verifarma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmaciasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly FarmacyService _farmacyService;

        public FarmaciasController(ApplicationDbContext context, FarmacyService farmacyService)
        {
            this._context = context;
            this._farmacyService = farmacyService;
        }

        // GET: api/<FarmaciasController>/
        [HttpGet]
        public async Task<IActionResult> GetFarmaciaCercana(float lat, float lon)
        {
            var dist = this._farmacyService.GetFarmaciaCercana(lat, lon);
            if (dist.Result < 0.0)
            {
                return NotFound();
            }

            return Ok(dist);
        }

        // GET api/<FarmaciasController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = this._farmacyService.GetFarmacia(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/<FarmaciasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Farmacia farmacia)
        {
            await this._context.Farmacias.AddAsync(farmacia);
            await this._context.SaveChangesAsync();
            return Ok();
        }

    }
}
