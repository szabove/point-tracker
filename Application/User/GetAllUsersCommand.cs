using Application.Abstraction;
using MediatR;

namespace Application.User
{
    public class GetAllUsersCommand
    {
        public class Query: IRequest<List<Domain.Model.User>>
        {
        }


        public class Handler : IRequestHandler<Query, List<Domain.Model.User>>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<List<Domain.Model.User>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _userRepository.GetAll();
            }
        }

    }
}
