using FCG.Catalog.Application.Dto.Game;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetPlataform
{
    public class GetPlataformQuery : IRequest<PlataformDto>
    {
        public int Id { get; set; }
    }
}
