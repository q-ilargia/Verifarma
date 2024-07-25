using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Verifarma.Data;
using Verifarma.Interfaces;
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
        private readonly IFarmacyService _farmacyService;

        public FarmaciasController(ApplicationDbContext context, IFarmacyService farmacyService)
        {
            this._context = context;
            this._farmacyService = farmacyService;
        }

        // GET: api/<FarmaciasController>/
        [HttpGet]
        public async Task<IActionResult> GetFarmaciaCercana(float lat, float lon)
        {
            var dist = await this._farmacyService.GetFarmaciaCercana(lat, lon);
            if (dist == null)
            {
                return NotFound();
            }

            return Ok(dist);
        }

        // GET api/<FarmaciasController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await this._farmacyService.GetFarmacia(id);
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
