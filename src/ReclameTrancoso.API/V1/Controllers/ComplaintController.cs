using System.Net.Mime;
using API.Utils;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.V1.Controllers;

[ApiController]
[Route(RouteUtils.API_V1_ROUTE + "complaints")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class ComplaintController : ControllerBase
{
    private readonly IUseCaseHandlerFactory _handlerFactory;

    public ComplaintController(IUseCaseHandlerFactory handlerFactory)
    {
        _handlerFactory = handlerFactory;
    }
    
    [HttpGet]
    [Authorize(Roles = "MANAGER")]
    [ProducesResponseType(typeof(PagedResponseDto<ComplaintDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] GetRequestPaginated request,
        CancellationToken cancellationToken)
    {
        var handler = _handlerFactory.CreateHandler<GetRequestPaginated, PagedResponseDto<ComplaintDto>>();
        var response = await handler.Handle(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] ComplaintCreateRequestDTO request, CancellationToken cancellationToken)
    {
        var handler = _handlerFactory.CreateHandler<ComplaintCreateRequestDTO, CreatedResponse>();
        var response = await handler.Handle(request, cancellationToken);
        return Created(string.Empty, response);
    }
    
    [HttpGet]
    [Authorize]
    [Route("{id}")]
    [ProducesResponseType(typeof(PagedResponseDto<ComplaintDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute]long id, [FromQuery] GetRequestPaginatedById request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        var handler = _handlerFactory.CreateHandler<GetRequestPaginatedById, PagedResponseDto<ComplaintDto>>();
        var response = await handler.Handle(request, cancellationToken);
        return Ok(response);
    }

    [HttpDelete]
    [Authorize]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id, CancellationToken cancellationToken)
    {

        var request = new DeleteRequest(id);
        var handler = _handlerFactory.CreateHandler<DeleteRequest>();
        await handler.Handle(request, cancellationToken);
        return NoContent();
    }

    [HttpPut]
    [Authorize]
    [Route("{id}")]
    [ProducesResponseType(typeof(ComplaintDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] ComplaintUpdateRequestDTO request, CancellationToken cancellationToken)
    {
        request.Id = id;
        var handler = _handlerFactory.CreateHandler<ComplaintUpdateRequestDTO, ComplaintDto>();
        var response = await handler.Handle(request, cancellationToken);
        return Ok(response);
    }
    
    
}