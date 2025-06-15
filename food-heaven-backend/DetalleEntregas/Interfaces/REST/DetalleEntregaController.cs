using System.Data;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using food_heaven_backend.DetalleEntregas.Application.CommandServices;
using food_heaven_backend.DetalleEntregas.Application.QueryServices;
using food_heaven_backend.DetalleEntregas.Domain.Models.Commands;
using food_heaven_backend.DetalleEntregas.Domain.Models.Exceptions;
using food_heaven_backend.DetalleEntregas.Domain.Models.Queries;
using food_heaven_backend.DetalleEntregas.Domain.Services;
using food_heaven_backend.DetalleEntregas.Interfaces.REST.Transform;

namespace food_heaven_backend.DetalleEntregas.Interfaces.REST;

[Route("api/v1/[controller]")]
[ApiController]
[Produces("application/json")]
public class DetalleEntregaController(
    IDetalleEntregaQueryService queryService,
    IDetalleEntregaCommandService commandService)
    : ControllerBase
{
    private readonly IDetalleEntregaQueryService _queryService = queryService;
    private readonly IDetalleEntregaCommandService _commandService = commandService;

    /// <summary>
    /// Get all detalles de entrega.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _queryService.Handle(new GetAllDetalleEntregaQuery());
        return result.Any()
            ? Ok(result.Select(DetalleEntregaResourceFromEntityAssembler.ToResourceFromEntity))
            : NotFound("No delivery details found.");
    }

    /// <summary>
    /// Get detalle de entrega by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id <= 0) return BadRequest("Invalid delivery ID.");

        var result = await _queryService.Handle(new GetDetalleEntregaByIdQuery(id));
        return result != null
            ? Ok(DetalleEntregaResourceFromEntityAssembler.ToResourceFromEntity(result))
            : NotFound($"Delivery detail with ID {id} not found.");
    }

    /// <summary>
    /// Create a new detalle de entrega.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public async Task<IActionResult> Post([FromBody] CreateDetalleEntregaCommand command)
    {
        if (command == null) return BadRequest("Delivery detail data cannot be null.");

        try
        {
            var entrega = await _commandService.Handle(command);
            return CreatedAtAction(nameof(Get), new { id = entrega.Id }, DetalleEntregaResourceFromEntityAssembler.ToResourceFromEntity(entrega));
        }
        catch (ValidationException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Update an existing detalle de entrega.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateDetalleEntregaCommand command)
    {
        if (id <= 0) return BadRequest("Invalid delivery ID.");

        try
        {
            var success = await _commandService.Handle(command, id);
            return success ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Delete a detalle de entrega by ID.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Invalid delivery ID.");

        try
        {
            var success = await _commandService.Handle(new DeleteDetalleEntregaCommand(id));
            return success ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
