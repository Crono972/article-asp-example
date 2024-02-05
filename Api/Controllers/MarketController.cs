using System.ComponentModel.DataAnnotations;
using Api.Repositories.Abstractions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
[Route("api/markets")]
[Route("api/v{v:apiVersion}/markets")]
public class MarketController(IMarketRepository marketRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        return Ok(await marketRepository.ListAsync());
    }

    [HttpGet("{boroughId}")]
    public async Task<IActionResult> GetAsync([Required(AllowEmptyStrings = false, ErrorMessage = "Invalid borough Id")] int boroughId)
    {
        return Ok(await marketRepository.GetFromAGivenBoroughAsync(boroughId));
    }
}