using FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGender;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGender;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditGender;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllGender;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetGender;
using FCG.Catalog.Domain.Entities;
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

        public GenderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertGender(AddGenderCommand addGenderCommand)
        {
            var gender = await _mediator.Send(addGenderCommand);
            return Created($"/api/gender/{gender.Id}", gender);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateGender([FromBody] EditGenderCommand editGenderCommand)
        {
            var gender = await _mediator.Send(editGenderCommand);

            return Ok(gender);
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteGender(int id)
        {
            var isDeleted = await _mediator.Send(new DeleteGenderCommand { Id = id });
            if (isDeleted)
            {
                return Ok("Gênero foi deletado com sucesso");
            }

            return NotFound();
        }

        [HttpGet("Get{id}")]
        public async Task<IActionResult> Get(int id)
        {
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
            var gender = await _mediator.Send(new GetAllGenderQuery());

            return Ok(gender);
        }
    }
}
