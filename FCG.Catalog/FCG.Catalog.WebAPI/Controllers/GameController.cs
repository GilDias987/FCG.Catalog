using FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.LinkDiscountGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.RequestPurchaseGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetGame;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Catalog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ADMINISTRADOR")]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="addGameCommand"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        [Authorize(Policy = "ADMINISTRADOR")]
        public async Task<IActionResult> IncluirGame(AddGameCommand addGameCommand)
        {
            var game = await _mediator.Send(addGameCommand);
            return Created($"/api/game/{game.Id}", game);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="editGameCommand"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [Authorize(Policy = "ADMINISTRADOR")]
        public async Task<IActionResult> UpdateGame([FromBody] EditGameCommand editGameCommand)
        {
            var game = await _mediator.Send(editGameCommand);

            return Ok(game);
        }

        /// <summary>
        /// Vincular Desconto
        /// </summary>
        /// <param name="linkDiscountGameCommand"></param>
        /// <returns></returns>
        [HttpPut("LinkDiscount")]
        [Authorize(Policy = "ADMINISTRADOR")]
        public async Task<IActionResult> LinkDiscount([FromBody] LinkDiscountGameCommand linkDiscountGameCommand)
        {
            var game = await _mediator.Send(linkDiscountGameCommand);

            return Ok(game);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete{id}")]
        [Authorize(Policy = "ADMINISTRADOR")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var isDeleted = await _mediator.Send(new DeleteGameCommand { Id = id });
            if (isDeleted)
            {
                return Ok("Jogo foi deletado com sucesso");
            }

            return NotFound();
        }

        /// <summary>
        /// Obter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var game = await _mediator.Send(new GetGameQuery { Id = id });

            return Ok(game);
        }

        /// <summary>
        /// Obter todos os jogos
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var game = await _mediator.Send(new GetAllGameQuery());

            return Ok(game);
        }

        /// <summary>
        /// Pedido para Comprar o Jogo
        /// </summary>
        /// <returns></returns>
        [HttpPost("RequestPaymentGame")]
        [Authorize]
        public async Task<IActionResult> RequestPaymentGame([FromBody] RequestPurchaseGameCommand requestPurchaseGameCommand)
        {
            var game = await _mediator.Send(requestPurchaseGameCommand);

            return Ok(game);
        }
    }
}
