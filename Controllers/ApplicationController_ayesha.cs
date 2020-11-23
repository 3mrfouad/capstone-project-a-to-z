using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AZLearn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController_ayesha :ControllerBase
    {
        // GET: api/<ApplicationController_ayesha>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1","value2" };
        }

        // GET api/<ApplicationController_ayesha>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ApplicationController_ayesha>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApplicationController_ayesha>/5
        [HttpPut("{id}")]
        public void Put(int id,[FromBody] string value)
        {
        }

        // DELETE api/<ApplicationController_ayesha>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
