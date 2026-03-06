using FCG.Catalog.Application.Dto.Game;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllUserGames
{
    public class GetAllUserGamesQuery : IRequest<List<GameDto>>
    {
        public int UserId { get; set; }
    }
}
