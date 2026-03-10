using FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGender;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGender;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditGender;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllGender;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetGender;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Catalog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ADMINISTRADOR")]
    public class GenderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GenderController> _logger;

        public GenderController(IMediator mediator, ILogger<GenderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertGender(AddGenderCommand addGenderCommand)
        {
            _logger.LogInformation("Iniciando inserção de novo gênero: {@GenderCommand}", addGenderCommand);

            var gender = await _mediator.Send(addGenderCommand);

            _logger.LogInformation("Gênero inserido com sucesso: {Id}", gender.Id);

            return Created($"/api/gender/{gender.Id}", gender);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateGender([FromBody] EditGenderCommand editGenderCommand)
        {
            _logger.LogInformation("Solicitação de atualização para o gênero ID: {Id}", editGenderCommand.Id);

            var gender = await _mediator.Send(editGenderCommand);

            return Ok(gender);
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteGender(int id)
        {
            _logger.LogWarning("Tentando excluir gênero com ID: {Id}", id);

            var isDeleted = await _mediator.Send(new DeleteGenderCommand { Id = id });
            if (isDeleted)
            {
                _logger.LogInformation("Gênero {Id} excluído com sucesso.", id);

                return Ok("Gênero foi deletado com sucesso");
            }

            _logger.LogWarning("Falha ao excluir: Gênero {Id} não encontrado.", id);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Buscando detalhes do gênero ID: {Id}", id);

            var gender = await _mediator.Send(new GetGenderQuery { Id = id });

            return Ok(gender);
        }

        /// <summary>
        /// Obter todos os gêneros
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Listando todos os gêneros.");

            var gender = await _mediator.Send(new GetAllGenderQuery());

            return Ok(gender);
        }
    }
}
