using System.Linq.Expressions;

namespace Application.Abstraction
{
    public interface ITransactionRepository
    {
        Task<Domain.Model.Transaction> Insert(Domain.Model.Transaction transaction);
        Task<List<Domain.Model.Transaction>> GetAll();
        Task<List<Domain.Model.Transaction>> GetByUserId(int userId);
        Task<List<Domain.Model.Transaction>> GetByExpresssion(Expression<Func<Domain.Model.Transaction, bool>> predicate);
        Task<bool> UpdateMultipleAsync(List<Domain.Model.Transaction> transactions);
    }
}
