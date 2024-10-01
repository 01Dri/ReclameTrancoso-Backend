using System.Net.Mime;
using API.Utils;
using Domain.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.DTOs.Resident;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.V1.Controllers;

[ApiController]
[Route(RouteUtils.API_V1_ROUTE + "resident")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class ResidentController : ControllerBase
{
    private readonly IUseCaseHandlerFactory _handlerFactory;

    public ResidentController(IUseCaseHandlerFactory handlerFactory)
    {
        _handlerFactory = handlerFactory;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResidentRegisterResponseDTO), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterAsync([FromBody] ResidentRegisterRequestDTO request, CancellationToken cancellationToken)
    {
        var handler = _handlerFactory.CreateHandler<ResidentRegisterRequestDTO, ResidentRegisterResponseDTO>();
        var response = await handler.Handle(request, cancellationToken);
        return Created(string.Empty, response);
    }
    
    [HttpGet]
    [Authorize]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResidentResponseDTO), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id, CancellationToken cancellationToken)
    {
        var handler = _handlerFactory.CreateHandler<GetByIdRequest, ResidentResponseDTO>();
        var response = await handler.Handle(new GetByIdRequest(id), cancellationToken);
        return Ok(response);
    }
}