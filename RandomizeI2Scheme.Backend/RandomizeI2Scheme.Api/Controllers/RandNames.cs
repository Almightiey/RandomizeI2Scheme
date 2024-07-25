using FullnameRandomizer;
using Microsoft.AspNetCore.Mvc;

namespace RandomizeI2Scheme.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RandNames : ControllerBase
{
    private readonly IRandomizeName _randomName;

    public RandNames(IRandomizeName randomizeName)
    {
        _randomName = randomizeName;
    }

    [HttpGet]
    public async Task<ActionResult> GetMaleRandomName()
    {
        var Name = _randomName.GetFullname(Gender.Male);

        return Ok(Name);
    }
    [HttpGet]
    public async Task<ActionResult> GetFemaleRandomName()
    {
        var name = _randomName.GetFullname(Gender.Female);
        return Ok(name);
    }
}

