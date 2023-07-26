using Bandit.NBS.Daemon.Commands;
using Bandit.NBS.Daemon.CommandHandlers;

namespace Bandit.NBS.Daemon.Extensions
{
    public interface ICommandHandlingBuilder
    {
        void AddHandler<T>(Predicate<ICommand> predicate) where T : class, ICommandHandler;
    }
}
