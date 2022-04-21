using EasyWeb.AspNetCore.Controllers;
using EasyWeb.Demo.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace EasyWeb.Demo.Controllers.v2;

/// <summary>
/// Lead Controller version 2
/// </summary>
[Route("v{ver:apiVersion}/leads")]
[ApiVersion("2.0")]
public class LeadController : BaseApiController
{
    /// <summary>
    /// Get Leads
    /// </summary>
    /// <returns>
    /// List of String
    /// </returns>
    [HttpGet(Name = "Get")]
    [ProducesResponseType(typeof(string[]), 200)]
    public IActionResult Get()
    {
        return Ok(new string[] {"Furkan", "Enis", "Okan", "Erol"});
    }

    [HttpPost(Name = "Insert")]
    [ProducesResponseType(typeof(string), 201)]
    public IActionResult Insert([FromBody]LeadRequestDto model)
    {
        return CreatedAtAction(nameof(Get), Request.RouteValues, "Inserted");
    }
}