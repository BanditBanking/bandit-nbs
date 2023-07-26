using Bandit.NBS.Daemon.Mappers;
using Bandit.NBS.Daemon.Models;
using Bandit.NBS.NpgsqlRepository.Repositories;

namespace Bandit.NBS.Daemon.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task AddTransactionAsync(AnalyticsTransaction transaction) => await _transactionRepository.AddTransactionAsync(transaction.ToModel());
    }
}
