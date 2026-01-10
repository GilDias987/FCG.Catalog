using FCG.Catalog.Application.Dto.Game;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.LinkDiscountGame
{
    public class LinkDiscountGameCommand : IRequest<GameDto>
    {
        public int Id { get; set; }
        public decimal? Discount { get; set; }
    }
}
