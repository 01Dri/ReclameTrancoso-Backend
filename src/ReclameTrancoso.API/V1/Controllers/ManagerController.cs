using System.Net.Mime;
using API.Utils;
using Domain.Interfaces;
using Domain.Models.DTOs.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.V1.Controllers;

[Authorize]
[ApiController]
[Route(RouteUtils.API_V1_ROUTE + "manager")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class ManagerController : ControllerBase
{
    private readonly IUseCaseHandlerFactory _handlerFactory;

    public ManagerController(IUseCaseHandlerFactory handlerFactory)
    {
        _handlerFactory = handlerFactory;
    }
    
    [HttpPost]
    [Authorize]
    [Route("add-comment")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ManagerAddCommentResponseDTO), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddCommentAsync([FromBody] ManagerAddCommentRequestDTO request,
        CancellationToken cancellationToken)
    {
        var handler = _handlerFactory.CreateHandler<ManagerAddCommentRequestDTO, ManagerAddCommentResponseDTO>();
        var response = await handler.Handle(request, cancellationToken);
        return Created(string.Empty, response);
    }

    
}