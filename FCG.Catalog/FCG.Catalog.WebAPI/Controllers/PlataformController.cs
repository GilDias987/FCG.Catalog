using FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddPlataform;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeletePlataform;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditPlataform;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllPlataform;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetPlataform;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Catalog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ADMINISTRADOR")]
    public class PlataformController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlataformController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertPlataform(AddPlataformCommand addPlataformCommand)
        {
            var plataform = await _mediator.Send(addPlataformCommand);

            return Created($"/api/plataform/{plataform.Id}", plataform);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePlataform([FromBody] EditPlataformCommand editPlataformCommand)
        {
            var plataform = await _mediator.Send(editPlataformCommand);

            return Ok(plataform);
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeletePlataform(int id)
        {
            var isDeleted = await _mediator.Send(new DeletePlataformCommand { Id = id });
            if (isDeleted)
            {
                return Ok("Plataforma foi deletado com sucesso");
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var plataform = await _mediator.Send(new GetPlataformQuery { Id = id });

            return Ok( plataform);
        }

        /// <summary>
        /// Obter todas as plataformas
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var plataform = await _mediator.Send(new GetAllPlataformQuery());

            return Ok(plataform);
        }
    }
}
