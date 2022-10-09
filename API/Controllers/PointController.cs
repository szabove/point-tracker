using Application.Point.AddPoints;
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
        public Task<string> Spend()
        {
            // TODO Implement Command
            return Task.FromResult("Points spent");
        }
    }
}
