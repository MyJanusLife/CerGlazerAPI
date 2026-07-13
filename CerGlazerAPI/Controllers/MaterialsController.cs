using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CerGlazerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        // GET: api/<MaterialsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MaterialsController>/5
        [HttpGet("{id}")]
        public string Get(int id, [FromQuery] string name)
        {
            return $"Located material {id} : for {name}";
        }

        // POST api/<MaterialsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MaterialsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MaterialsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
