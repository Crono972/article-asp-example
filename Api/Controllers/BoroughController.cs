using System.ComponentModel.DataAnnotations;
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
    public async Task<IActionResult> GetAsync([Required(AllowEmptyStrings = false, ErrorMessage = "Invalid borough Id")]int boroughId)
    {
        var borough = await boroughRepository.GetAsync(boroughId);
        if (borough == null)
        {
            return NotFound();
        }
        return Ok(borough);
    }

    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        return Ok(await boroughRepository.ListAsync());
    }
}