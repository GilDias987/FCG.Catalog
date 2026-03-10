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
        private readonly ILogger<PlataformController> _logger;

        public PlataformController(IMediator mediator, ILogger<PlataformController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertPlataform(AddPlataformCommand addPlataformCommand)
        {
            _logger.LogInformation("Iniciando inserção de nova plataforma: {Title}", addPlataformCommand.Title);

            var plataform = await _mediator.Send(addPlataformCommand);

            _logger.LogInformation("Plataforma inserida com sucesso. ID: {Id}", plataform.Id);

            return Created($"/api/plataform/{plataform.Id}", plataform);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePlataform([FromBody] EditPlataformCommand editPlataformCommand)
        {
            _logger.LogInformation("Atualizando plataforma ID: {Id}", editPlataformCommand.Id);

            var plataform = await _mediator.Send(editPlataformCommand);

            return Ok(plataform);
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeletePlataform(int id)
        {
            _logger.LogWarning("Tentativa de exclusão da plataforma ID: {Id}", id);

            var isDeleted = await _mediator.Send(new DeletePlataformCommand { Id = id });
            if (isDeleted)
            {
                _logger.LogInformation("Plataforma {Id} deletada com sucesso", id);

                return Ok("Plataforma foi deletado com sucesso");
            }

            _logger.LogWarning("Falha ao deletar: Plataforma {Id} não encontrada", id);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Buscando plataforma ID: {Id}", id);

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
            _logger.LogInformation("Consultando todas as plataformas");

            var plataform = await _mediator.Send(new GetAllPlataformQuery());

            return Ok(plataform);
        }
    }
}
