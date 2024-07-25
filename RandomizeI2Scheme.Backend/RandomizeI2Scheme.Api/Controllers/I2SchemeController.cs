using AutoMapper;
using I2Scheme.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using I2Scheme.Persistece.Models;
using RandomizeI2Scheme.Api.Models.Dto;

namespace RandomizeI2scheme.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class I2schemeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISchemeDataManager _schemeDataManager;

    public I2schemeController(IMapper mapper,
        ISchemeDataManager schemeDataManager)
    {
        _mapper = mapper;
        _schemeDataManager = schemeDataManager;
    }

    [HttpGet]
    public async Task<ActionResult<List<I2SchemeDto>>> GetAllSchemes(CancellationToken token)
    {
        var schemes = await _schemeDataManager.GetAllSchemesAsync(token);

        var vmScheme = _mapper.Map<List<I2SchemeDto>>(schemes);
        return vmScheme;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<I2SchemeDto>> GetById(int id, CancellationToken token)
    {
        var entity = await _schemeDataManager.GetSchemeByIdAsync(id, token);

        var vmScheme = _mapper.Map<I2SchemeDto>(entity);
        return vmScheme;
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<I2SchemeDto>> GetByName(string name, CancellationToken token)
    {
        var entity = await _schemeDataManager.GetSchemeByName(name, token);
        var vmScheme = _mapper.Map<I2SchemeDto>(entity);
        return vmScheme;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken token)
    {
        await _schemeDataManager.DeleteSchemeAsync(id, token);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<I2scheme>> Post([FromBody] I2SchemeDto I2schemeDto, CancellationToken token)
    {
        if (I2schemeDto == null)
            return BadRequest("I2schemeExport not provided");

        var model = _mapper.Map<I2scheme>(I2schemeDto);
       var scheme = await _schemeDataManager.CreateSchemeAsync(model, token);
       var vmScheme = _mapper.Map<I2SchemeDto>(scheme);
        return CreatedAtAction(nameof(Post), vmScheme);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<I2scheme>> Update(int id, [FromBody] I2SchemeDto I2schemeDto, CancellationToken token)
    {
        if (I2schemeDto == null)
            return BadRequest("I2schemeExport not provided");

        var model = _mapper.Map<I2scheme>(I2schemeDto);
        await _schemeDataManager.UpdateSchemeAsync(id, model, token);

        return AcceptedAtAction(nameof(Update), null);

    }
}