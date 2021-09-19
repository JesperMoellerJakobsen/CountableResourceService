using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Model;
using Domain.Model;
using Domain.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CounterController : ControllerBase
    {
        private readonly ICounterService _counterService;

        public CounterController(ICounterService counterService)
        {
            _counterService = counterService;
        }

        [HttpGet]
        public async Task<ICounter> Get()
        {
            return await _counterService.GetCounter();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] CounterPatchInput input)
        {
            if (!ModelState.IsValid || input.Version == null)
                return BadRequest();

            var operationSuccess = false;

            if (input.PatchOption == CounterPatchOption.Increment)
                operationSuccess = await _counterService.TryIncrement(input.Version);

            if (input.PatchOption == CounterPatchOption.Decrement)
                operationSuccess = await _counterService.TryDecrement(input.Version);

            return !operationSuccess ? Conflict() : Ok();
        }
    }
}
