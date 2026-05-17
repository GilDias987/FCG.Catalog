using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGameLibrary
{
    public class AddGameLibraryCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int GameId { get; set; }

    }
}
