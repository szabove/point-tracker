using Application.Transactions;
using Domain.Model;
using MediatR;

namespace API.Controllers
{
    public class TransactionController : BaseController
    {
        public TransactionController(IMediator mediator): base(mediator) { }

        public async Task<List<Transaction>> GetTransactions(GetTransactionsQuery.Query request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
