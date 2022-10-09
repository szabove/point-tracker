using Application.Abstraction;
using MediatR;

namespace Application.Transactions
{
    public class GetTransactionsQuery
    {
        public class Query: IRequest<List<Domain.Model.Transaction>> {}

        public class Handler : IRequestHandler<Query, List<Domain.Model.Transaction>>
        {
            private readonly ITransactionRepository _transactionRepository;

            public Handler(ITransactionRepository transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }
            public async Task<List<Domain.Model.Transaction>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _transactionRepository.GetAll();
            }
        }
    }
}
