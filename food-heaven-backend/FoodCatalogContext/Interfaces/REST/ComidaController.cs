using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using food_heaven_backend.FoodCatalogContext.Domain.Models.Commands;
using food_heaven_backend.FoodCatalogContext.Domain.Services;
using food_heaven_backend.FoodCatalogContext.Interfaces.REST.Transform;
using food_heaven_backend.FoodCatalogContext.Domain.Models.Queries;
using food_heaven_backend.FoodCatalogContext.Domain.Models.Exceptions;

namespace food_heaven_backend.FoodCatalogContext.Interfaces.REST
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ComidaController(IComidaQueryService comidaQueryService, IComidaCommandService comidaCommandService) : ControllerBase
    {
        private readonly IComidaQueryService _comidaQueryService = comidaQueryService;
        private readonly IComidaCommandService _comidaCommandService = comidaCommandService;

        // GET: api/Comida
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _comidaQueryService.Handle(new GetAllComidaQuery());
            return result.Any() 
                ? Ok(result.Select(ComidaResourceFromEntityAssembler.ToResourceFromEntity)) 
                : NotFound("No comidas found.");
        }

        // GET: api/Comida/{name}
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("Name cannot be empty.");

            var result = await _comidaQueryService.Handle(new GetComidabyNameQuery(name));
            return result != null 
                ? Ok(ComidaResourceFromEntityAssembler.ToResourceFromEntity(result)) 
                : NotFound($"Comida with name '{name}' not found.");
        }

        // POST: api/Comida
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Post([FromBody] CreateComidaCommand command)
        {
            if (command == null) return BadRequest("Comida data cannot be null.");

            try
            {
                await _comidaCommandService.Handle(command);
                return StatusCode(StatusCodes.Status201Created);
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
                return Conflict("A comida with the same name already exists.");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Comida/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateComidaCommand command)
        {
            if (id <= 0) return BadRequest("Invalid comida ID.");

            try
            {
                await _comidaCommandService.Handle(command, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Comida/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Invalid comida ID.");

            try
            {
                await _comidaCommandService.Handle(new DeleteComidaCommand(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
