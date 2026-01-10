using FCG.Catalog.Application.Dto.Game;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditGame
{
    public class EditGameCommand : IRequest<GameDto>
    {
        public int Id { get; set; }
        public required string TiTle { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public int GenderId { get; set; }
        public int PlataformId { get; set; }
    }
}
