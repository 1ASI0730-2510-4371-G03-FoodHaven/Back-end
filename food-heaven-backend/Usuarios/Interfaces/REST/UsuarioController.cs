using System.Data;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using food_heaven_backend.Usuarios.Application.CommandServices;
using food_heaven_backend.Usuarios.Application.QueryServices;
using food_heaven_backend.Usuarios.Domain.Models.Commands;
using food_heaven_backend.Usuarios.Domain.Models.Exceptions;
using food_heaven_backend.Usuarios.Domain.Models.Queries;
using food_heaven_backend.Usuarios.Domain.Services;
using food_heaven_backend.Usuarios.Interfaces.REST.Transform;

namespace food_heaven_backend.Usuarios.Interfaces.REST;

[Route("api/v1/[controller]")]
[ApiController]
[Produces("application/json")]
public class UsuarioController(
    IUsuarioQueryService usuarioQueryService,
    IUsuarioCommandService usuarioCommandService)
    : ControllerBase
{
    private readonly IUsuarioQueryService _usuarioQueryService = usuarioQueryService;
    private readonly IUsuarioCommandService _usuarioCommandService = usuarioCommandService;

    /// <summary>
    /// Get all active usuarios.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _usuarioQueryService.Handle(new GetAllUsuariosQuery());
        return result.Any()
            ? Ok(result.Select(UsuarioResourceFromEntityAssembler.ToResourceFromEntity))
            : NotFound("No usuarios found.");
    }

    /// <summary>
    /// Get usuario by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id <= 0) return BadRequest("Invalid usuario ID.");

        var result = await _usuarioQueryService.Handle(new GetUsuarioByIdQuery(id));
        return result != null
            ? Ok(UsuarioResourceFromEntityAssembler.ToResourceFromEntity(result))
            : NotFound($"Usuario with ID {id} not found.");
    }

    /// <summary>
    /// Create a new usuario.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public async Task<IActionResult> Post([FromBody] CreateUsuarioCommand command)
    {
        if (command == null) return BadRequest("Usuario data cannot be null.");

        try
        {
            var usuario = await _usuarioCommandService.Handle(command);
            return CreatedAtAction(nameof(Get), new { id = usuario.Id }, UsuarioResourceFromEntityAssembler.ToResourceFromEntity(usuario));
        }
        catch (ValidationException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        catch (DuplicateNameException)
        {
            return Conflict("A usuario with the same email already exists.");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Update an existing usuario by ID.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateUsuarioCommand command)
    {
        if (id <= 0) return BadRequest("Invalid usuario ID.");

        try
        {
            var success = await _usuarioCommandService.Handle(command, id);
            return success ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Delete a usuario by ID.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Invalid usuario ID.");

        try
        {
            var success = await _usuarioCommandService.Handle(new DeleteUsuarioCommand(id));
            return success ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
