using System.Net.Mime;
using API.Utils;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using ReclameTrancoso.Domain.Interfaces.Auth;

namespace API.V1.Controllers;
[ApiController]
[Route(RouteUtils.API_V1_ROUTE + "auth")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class AuthController :   ControllerBase
{
    private readonly IUseCaseHandlerFactory _handlerFactory;
    private readonly ITokenService<User, TokenResponseDTO> _tokenService;

    public AuthController(IUseCaseHandlerFactory handlerFactory, ITokenService<User, TokenResponseDTO> tokenService)
    {
        _handlerFactory = handlerFactory;
        _tokenService = tokenService;
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
    
    [HttpPost]
    [Route("refresh-token")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(TokenResponseDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequestDTO request, CancellationToken cancellationToken)
    {
        var handler = _handlerFactory.CreateHandler<RefreshTokenRequestDTO, TokenResponseDTO>();
        var response = await handler.Handle(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("validate-token")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(TokenResponseDTO), StatusCodes.Status200OK)]
    public IActionResult ValidateToken([FromHeader(Name = "Authorization")] string token, CancellationToken cancellationToken)
    {
        try
        {
            if (!_tokenService.ValidateToken(token))
            {
                return Unauthorized(new { message = "Token inválido ou expirado" });
            }
            return Ok(new { message = "Token válido" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao validar o token", error = ex.Message });
        }
    }
}