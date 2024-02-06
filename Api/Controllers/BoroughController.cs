using System.ComponentModel.DataAnnotations;
using System.Net;
using Api.Model;
using Api.Repositories.Abstractions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
[Route("api/boroughs")]
[Route("api/v{v:apiVersion}/boroughs")]
public class BoroughController(IBoroughRepository boroughRepository) : ControllerBase
{
    [HttpGet("{boroughId}")]
    [ProducesResponseType(typeof(Borough), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([Required(AllowEmptyStrings = false, ErrorMessage = "Invalid borough Id")]int boroughId)
    {
        var borough = await boroughRepository.GetAsync(boroughId);
        if (borough == null)
        {
            return NotFound();
        }
        return Ok(borough);
    }

    [HttpGet, ProducesResponseType(typeof(IList<Borough>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListAsync()
    {
        return Ok(await boroughRepository.ListAsync());
    }

    [HttpGet("light"), ProducesResponseType(typeof(IList<LightBorough>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListLighBoroughAsync()
    {
        return Ok((await boroughRepository.ListAsync()).Select(b => new LightBorough
        {
            ArinseeCode = b.ArinseeCode,
            BoroughNumber = b.BoroughNumber,
            Name = b.Name,
            PerimeterSize = b.PerimeterSize
        }));
    }
}