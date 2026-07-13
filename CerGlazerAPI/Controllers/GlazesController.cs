using CerGlazerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CerGlazerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlazesController : ControllerBase
    {
        private readonly ILogger<GlazesController> _logger;

        private readonly List<Glaze> _glazes = new List<Glaze>
        {
            new Glaze { Id = 1, Name = "Glaze1", IdealConeCategory = "Low" },
            new Glaze { Id = 2, Name = "Glaze2", IdealConeCategory = "Medium" },
            new Glaze { Id = 3, Name = "Glaze3", IdealConeCategory = "High" }
        };

        public GlazesController(ILogger<GlazesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetGlazes()
        {
            //returns a list of glazes
            return Ok(new List<string> { "Glaze1", "Glaze2", "Glaze3" });
        }

        [HttpGet("{id}")]
        public IActionResult GetGlazeById(int id)
        {
            var shirt = _glazes.FirstOrDefault(g => g.Id == id) ??
                new Glaze { Id = 0, Name = "Not Found", IdealConeCategory = "N/A" };

            return Ok(shirt);
        }

        [HttpPost]
        public IActionResult CreateGlazes([FromBody] Glaze ceramicGlaze)
        {
            //returns a list of glazes
            return Ok(new List<string> { "Glaze1", "Glaze2", "Glaze3" });
        }

    }
}
