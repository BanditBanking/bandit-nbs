using Bandit.NBS.Daemon.Clients;
using Bandit.NBS.Daemon.Commands;

namespace Bandit.NBS.Daemon.CommandHandlers
{
    public interface ICommandHandler
    {
        Task HandleAsync(SslClient client, ICommand command, CancellationToken cancellationToken);
    }
}
