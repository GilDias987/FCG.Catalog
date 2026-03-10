using FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.LinkDiscountGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.RequestPurchaseGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllGame;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllUserGames;
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
        private readonly ILogger<GameController> _logger;

        public GameController(IMediator mediator, ILogger<GameController> logger)
        {
            _mediator = mediator;
            _logger = logger;
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
            _logger.LogInformation("Iniciando inclusão de novo jogo: {GameTitle}", addGameCommand.Title);

            var game = await _mediator.Send(addGameCommand);

            _logger.LogInformation("Jogo incluído com sucesso. ID: {GameId}", game.Id);

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
            _logger.LogInformation("Atualizando jogo ID: {GameId}", editGameCommand.Id);

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
            _logger.LogInformation("Vinculando desconto ao jogo ID: {GameId}", linkDiscountGameCommand.Id);

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
            _logger.LogWarning("Tentativa de exclusão do jogo ID: {GameId}", id);

            var isDeleted = await _mediator.Send(new DeleteGameCommand { Id = id });
            if (isDeleted)
            {
                _logger.LogInformation("Jogo ID: {GameId} deletado com sucesso", id);

                return Ok("Jogo foi deletado com sucesso");
            }

            _logger.LogWarning("Falha ao deletar: Jogo ID: {GameId} não encontrado", id);

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
            _logger.LogDebug("Buscando detalhes do jogo ID: {GameId}", id);

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
            _logger.LogInformation("Buscando lista completa de jogos");

            var game = await _mediator.Send(new GetAllGameQuery());

            return Ok(game);
        }

        /// <summary>
        /// Listar jogos da Biblioteca do Usuário
        /// </summary>
        /// <returns></returns>
        [HttpGet("UsersGameLibrary/{userId}")]
        [Authorize]
        public async Task<IActionResult> UsersGameLibrary(int userId)
        {
            _logger.LogInformation("Buscando biblioteca do usuário ID: {UserId}", userId);

            var games = await _mediator.Send(new GetAllUserGamesQuery { UserId = userId});

            return Ok(games);
        }

        /// <summary>
        /// Pedido para Comprar o Jogo
        /// </summary>
        /// <returns></returns>
        [HttpPost("RequestPaymentGame")]
        [Authorize]
        public async Task<IActionResult> RequestPaymentGame([FromBody] RequestPurchaseGameCommand requestPurchaseGameCommand)
        {
            _logger.LogInformation("Solicitação de compra iniciada para Jogo ID: {GameId} pelo Usuário: {UserId}",
                requestPurchaseGameCommand.GameId, requestPurchaseGameCommand.UserId);

            var game = await _mediator.Send(requestPurchaseGameCommand);

            return Ok(game);
        }

 
    }
}
