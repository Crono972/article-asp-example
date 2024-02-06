using System.ComponentModel.DataAnnotations;
using Api.Model;
using Api.Repositories.Abstractions;
using Api.Serialization;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiVersion(2.0)]
[Route("api/markets")]
[Route("api/v{v:apiVersion}/markets")]
public class MarketController(IMarketRepository marketRepository) : ControllerBase
{
    [HttpGet, ProducesResponseType(typeof(IList<Market>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListAsync()
    {
        return Ok(await marketRepository.ListAsync());
    }

    [HttpGet("{boroughId}"), ProducesResponseType(typeof(IList<Market>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([Required(AllowEmptyStrings = false, ErrorMessage = "Invalid borough Id")] int boroughId)
    {
        return Ok(await marketRepository.GetFromAGivenBoroughAsync(boroughId));
    }

    [HttpGet, MapToApiVersion(2.0), ProducesResponseType(typeof(IList<Market>), StatusCodes.Status200OK)]
    [ForceSerialization(SerializationStrategy.SnakeCase)]
    public async Task<IActionResult> ListAsyncV2()
    {
        return Ok(await marketRepository.ListAsync());
    }

    [HttpGet("{boroughId}"), MapToApiVersion(2.0), ProducesResponseType(typeof(IList<Market>), StatusCodes.Status200OK)]
    [ForceSerialization(SerializationStrategy.SnakeCase)]
    public async Task<IActionResult> GetAsyncV2([Required(AllowEmptyStrings = false, ErrorMessage = "Invalid borough Id")] int boroughId)
    {
        return Ok(await marketRepository.GetFromAGivenBoroughAsync(boroughId));
    }
}
