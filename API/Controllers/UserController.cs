using Application.User;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator) { }
        public async Task<List<User>> GetAll(GetAllUsersCommand.Query request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
