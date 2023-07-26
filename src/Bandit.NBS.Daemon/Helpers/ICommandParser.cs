using Bandit.NBS.Daemon.Commands;

namespace Bandit.NBS.Daemon.Helpers
{
    public interface ICommandParser
    {
        ICommand Parse(string rawCommand);
    }
}
