using Application.Abstraction;
using Domain.Model;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PointTrackerDbContext _dbContext;

        public TransactionRepository(PointTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Transaction>> GetAll()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<List<Transaction>> GetByUserId(int userId)
        {
            return await _dbContext.Transactions.Where(x => x.UserId == userId && x.Points != 0).ToListAsync();
        }

        public async Task<List<Transaction>> GetByExpresssion(Expression<Func<Transaction, bool>> predicate)
        {
            return await _dbContext.Transactions.Where(predicate).ToListAsync();
        }

        public async Task<Transaction> Insert(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> UpdateMultipleAsync(List<Transaction> transactions)
        {
            _dbContext.Transactions.UpdateRange(transactions);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
