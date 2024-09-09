using System.Net.Mime;
using API.Utils;
using Domain.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.DTOs.Building;
using Microsoft.AspNetCore.Mvc;

namespace API.V1.Controllers;

[ApiController]
[Route(RouteUtils.API_V1_ROUTE + "buildings")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class BuildingController : ControllerBase
{
    private readonly IUseCaseHandlerFactory _handlerFactory;

    public BuildingController(IUseCaseHandlerFactory handlerFactory)
    {
        _handlerFactory = handlerFactory;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(BuildingResponseDTO), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var handler = _handlerFactory.CreateHandler<GetRequest, BuildingResponseDTO>();
        var response = await handler.Handle(null, cancellationToken);
        return Ok(response);
    }
}