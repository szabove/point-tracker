using Application.Abstraction;
using MediatR;
using Domain.Model;

namespace Application.Transactions
{
    public class GetTransactionsQuery
    {
        public class Query: IRequest<List<Transaction>> {}

        public class Handler : IRequestHandler<Query, List<Transaction>>
        {
            private readonly ITransactionRepository _transactionRepository;

            public Handler(ITransactionRepository transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }
            public async Task<List<Transaction>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _transactionRepository.GetAll();
            }
        }
    }
}
