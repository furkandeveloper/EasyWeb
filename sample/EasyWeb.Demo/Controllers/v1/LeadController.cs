using EasyWeb.AspNetCore.Controllers;
using EasyWeb.Demo.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace EasyWeb.Demo.Controllers.v1;

[Route("v{ver:apiVersion}/leads")]
[ApiVersion("1.0")]
public class LeadController : BaseApiController
{
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