using FCG.Catalog.Application.Dto.Game;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetGame
{
    public class GetGameQuery : IRequest<GameDto>
    {
        public int Id { get; set; }
    }
}
