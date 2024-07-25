using FullnameRandomizer;
using Microsoft.AspNetCore.Mvc;
using RegularExpressionsRandomizer;

namespace RandomizeI2Scheme.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RegexGeneratorController : Controller
    {
        private readonly IRegexRandomizer _stringGenegator;

        public RegexGeneratorController(IRegexRandomizer stringGenegator)
        {
            _stringGenegator = stringGenegator;
        }

        [HttpGet]
        public async Task<ActionResult> GetRandomString(string regex)
        {
            var randString = _stringGenegator.GetData(regex);

            return Ok(randString);
        }
    }
}
