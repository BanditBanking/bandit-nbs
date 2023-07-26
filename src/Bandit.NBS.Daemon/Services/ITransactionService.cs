using Bandit.NBS.Daemon.Models;
using Bandit.NBS.Daemon.Models.DTOs;

namespace Bandit.NBS.Daemon.Services
{
    public interface ITransactionService
    {
        Task AddTransactionAsync(AnalyticsTransaction transaction);
    }
}
