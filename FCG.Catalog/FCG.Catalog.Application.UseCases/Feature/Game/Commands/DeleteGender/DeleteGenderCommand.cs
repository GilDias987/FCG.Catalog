using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGender
{
    public class DeleteGenderCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
