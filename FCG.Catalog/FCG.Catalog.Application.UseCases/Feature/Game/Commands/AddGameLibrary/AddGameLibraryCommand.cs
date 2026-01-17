using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGameLibrary
{
    public class AddGameLibraryCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int GameId { get; set; }

    }
}
