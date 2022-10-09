using Application.Abstraction;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Point.AddPoints
{
    public class AddPointsCommand
    {
        public class Command: IRequest
        {
            public int UserId { get; set; }
            public string PayerName { get; set; }
            public int Points { get; set; }
            public DateTime TimeStamp { get; set; }
        }

        public class CommandValidator: AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.UserId).GreaterThan(0);
                RuleFor(c => c.PayerName).NotEmpty();
                RuleFor(c => c.Points).NotEmpty().NotEqual(0);
                RuleFor(c => c.TimeStamp).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ITransactionRepository _transactionRepository;
            private readonly IMapper _mapper;

            public Handler(ITransactionRepository transactionRepository, IMapper mapper)
            {
                _transactionRepository = transactionRepository;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // TODO Check if user exists
                // what to do if the user doesnt exists

                var pointsToProcess = request.Points;
                // Check if points is positive
                if (pointsToProcess > 0)
                {
                    await _transactionRepository.Insert(_mapper.Map<Domain.Model.Transaction>(request));
                }
                else
                {
                    var userTransactions = await _transactionRepository.GetByExpresssion(
                            x=>x.UserId == request.UserId &&
                            x.PayerName.Equals(request.PayerName,StringComparison.InvariantCultureIgnoreCase) &&
                            x.Points != 0
                        );

                    // Making sure the subtraction can be performed with checks
                    if (!userTransactions.Any())
                    {
                        // TODO write custom exception for this
                        throw new Exception($"First transaction for {request.PayerName} shouldn't be negative");
                    }
                    if (userTransactions.Sum(x=>x.Points) < pointsToProcess)
                    {
                        // TODO write custom exception for this
                        throw new Exception("Insufficient points");
                    }

                    var transactionsToUpdate = new List<Domain.Model.Transaction>();
                    foreach (var transaction in userTransactions.OrderBy(x=>x.TimeStamp))
                    {
                        var result = pointsToProcess += transaction.Points;
                        transaction.Points = result > 0 ? result : 0;
                        transactionsToUpdate.Add(transaction);
                        if (result > 0)
                        {
                            break;
                        }
                    }

                    await _transactionRepository.UpdateMultipleAsync(transactionsToUpdate);
                }

                return Unit.Value;
            }
        }
    }
}
