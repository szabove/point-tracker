using Application.Abstraction;
using Domain.Model;
using FluentValidation;
using MediatR;

namespace Application.Point.SpendPoint
{
    public class SpendPointCommand
    {
        public class Command: IRequest<List<SpentPointsDto>>
        {
            public int UserId { get;set; }
            public int Points { get; set; }
        }

        public class CommandValidator: AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Points).NotEmpty().GreaterThan(0);
                RuleFor(x => x.UserId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, List<SpentPointsDto>>
        {
            private readonly ITransactionRepository _transactionRepository;

            public Handler(ITransactionRepository transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }
            public async Task<List<SpentPointsDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                // TODO Check if user exists
                // what to do if the user doesnt exists

                var pointsToProcess = request.Points;

                var userTransactions = await _transactionRepository.GetByExpresssion(
                            x => x.UserId == request.UserId &&
                            x.Points != 0
                        );
                if (userTransactions.Sum(x => x.Points) < pointsToProcess)
                {
                    // TODO write custom exception for this
                    throw new Exception("Insufficient points");
                }

                var transactionsToUpdate = new List<Transaction>();
                var spentPointsList = new List<SpentPointsDto>();
                var orderedTransactionByTimestamp = userTransactions.OrderBy(x => x.TimeStamp);
                foreach (var transaction in orderedTransactionByTimestamp)
                {
                    var pointsSpent = 0;
                    if (pointsToProcess < transaction.Points)
                    {
                        transaction.Points -= pointsToProcess;
                        pointsSpent = pointsToProcess;
                    }
                    else
                    {
                        pointsToProcess -= transaction.Points;
                        pointsSpent = transaction.Points;
                    }
                    spentPointsList.Add(new SpentPointsDto
                    {
                        Payer = transaction.Payer,
                        Points = -pointsSpent
                    });
                    transactionsToUpdate.Add(transaction);

                    if (pointsToProcess == 0)
                    {
                        break;
                    }
                }

                await _transactionRepository.UpdateMultipleAsync(transactionsToUpdate);

                var result = spentPointsList.GroupBy(x => x.Payer, x => x.Points)
                    .Select(y => new SpentPointsDto
                    {
                        Payer = y.Key,
                        Points = y.Sum()
                    }).ToList();

                return result;
            }
        }
    }
}