using Application.Point.AddPoints;
using Application.Point.PointBalance;
using Application.Point.SpendPoint;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PointController : BaseController
    {
        public PointController(IMediator mediator) : base(mediator) {}

        [HttpPost]
        public async Task<Unit> Add(AddPointsCommand.Command request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        public async Task<List<SpentPointsDto>> Spend(SpendPointCommand.Command request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpGet]
        public async Task<Dictionary<string, int>> GetBalance(GetPointBalanceQuery.Query request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
