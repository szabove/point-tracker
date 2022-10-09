using System.Linq.Expressions;
using Domain.Model;


namespace Application.Abstraction
{
    public interface ITransactionRepository
    {
        Task<Transaction> Insert(Transaction transaction);
        Task<List<Transaction>> GetAll();
        Task<List<Transaction>> GetByUserId(int userId);
        Task<List<Transaction>> GetByExpresssion(Expression<Func<Transaction, bool>> predicate);
        Task<bool> UpdateMultipleAsync(List<Transaction> transactions);
    }
}
