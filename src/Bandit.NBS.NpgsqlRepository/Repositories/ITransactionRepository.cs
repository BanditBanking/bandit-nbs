using Bandit.NBS.NpgsqlRepository.Models;

namespace Bandit.NBS.NpgsqlRepository.Repositories
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(Transaction transaction);
    }
}
