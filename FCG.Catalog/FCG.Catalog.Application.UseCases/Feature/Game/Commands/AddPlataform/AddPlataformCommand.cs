using FCG.Catalog.Application.Dto.Game;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddPlataform
{
    public class AddPlataformCommand : IRequest<PlataformDto>
    {
        public required string Title { get; set; }
    }
}
