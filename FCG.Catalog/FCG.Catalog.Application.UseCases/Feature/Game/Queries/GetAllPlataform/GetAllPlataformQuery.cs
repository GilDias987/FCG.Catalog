using FCG.Catalog.Application.Dto.Game;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllPlataform
{
    public class GetAllPlataformQuery : IRequest<List<PlataformDto>>
    {
    }
}
