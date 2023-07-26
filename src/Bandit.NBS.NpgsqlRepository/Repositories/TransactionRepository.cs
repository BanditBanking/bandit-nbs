using Bandit.NBS.NpgsqlRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Bandit.NBS.NpgsqlRepository.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly NpgsqlDbContext _context;
        private readonly DbSet<Transaction> _transactions;

        public TransactionRepository(NpgsqlDbContext context)
        {
            _context = context;
            _transactions = context.Transactions;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
