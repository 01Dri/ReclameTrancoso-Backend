using System.Net.Mime;
using API.Utils;
using Domain.Interfaces;
using Domain.Models.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.V1.Controllers;
[ApiController]
[Route(RouteUtils.API_V1_ROUTE + "auth")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class AuthController :   ControllerBase
{
    private readonly IUseCaseHandlerFactory _handlerFactory;

    public AuthController(IUseCaseHandlerFactory handlerFactory)
    {
        _handlerFactory = handlerFactory;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(TokenResponseDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO request, CancellationToken cancellationToken)
    {
        var handler = _handlerFactory.CreateHandler<LoginRequestDTO, TokenResponseDTO>();
        var response = await handler.Handle(request, cancellationToken);
        return Ok(response);
    }
}