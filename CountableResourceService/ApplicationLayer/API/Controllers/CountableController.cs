using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountableController : ControllerBase
    {
        public CountableController()
        {
        }

        [HttpGet]
        public int Get()
        {
            var rng = new Random();
            return rng.Next();
        }

        [HttpGet]
        [Route("Increment")]
        public int Increment()
        {
            var rng = new Random();
            return rng.Next();
        }

        [HttpGet]
        [Route("Decrement")]
        public int Decrement()
        {
            var rng = new Random();
            return rng.Next() * -1;
        }
    }
}
