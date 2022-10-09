using Application.Abstraction;
using FluentValidation;
using MediatR;

namespace Application.Point.PointBalance
{
    public class GetPointBalanceQuery
    {
        public class Query : IRequest<Dictionary<string, int>>
        {
            public int UserId { get; set; }
        }

        public class QueryValidator: AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.UserId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, Dictionary<string, int>>
        {
            private readonly ITransactionRepository _transactionRepository;

            public Handler(ITransactionRepository transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }
            public async Task<Dictionary<string, int>> Handle(Query request, CancellationToken cancellationToken)
            {
                var transactions = await _transactionRepository.GetByExpresssion(x=> x.UserId==request.UserId && x.Points != 0);

                var group = transactions.GroupBy(x=>x.Payer, x=>x.Points).ToDictionary(x => x.Key, x => x.Sum());

                return group;
            }
        }

    }
}
