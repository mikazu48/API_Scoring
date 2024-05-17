using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Scoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassDetailController : ControllerBase
    {
        // GET: api/<ClassSController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ClassSController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClassSController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClassSController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClassSController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
