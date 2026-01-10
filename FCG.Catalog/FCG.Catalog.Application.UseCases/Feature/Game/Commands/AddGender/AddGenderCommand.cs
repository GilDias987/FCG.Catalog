using FCG.Catalog.Application.Dto.Game;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGender
{
    public class AddGenderCommand : IRequest<GenderDto>
    {
        public required string Title { get; set; }
    }
}
