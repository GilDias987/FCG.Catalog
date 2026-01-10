using FCG.Catalog.Application.Dto.Game;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditGender
{
    public class EditGenderCommand : IRequest<GenderDto>
    {
        public int Id { get; set; }
        public required string Title { get; set; }
    }
}
