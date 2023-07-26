namespace Bandit.NBS.Daemon.Commands
{
    public class RawCommand : ICommand
    {
        public string Type { get; set; } = nameof(RawCommand);
    }
}
