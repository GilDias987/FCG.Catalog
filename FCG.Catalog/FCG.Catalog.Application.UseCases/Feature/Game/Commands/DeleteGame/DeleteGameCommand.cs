using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGame
{
    public class DeleteGameCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
